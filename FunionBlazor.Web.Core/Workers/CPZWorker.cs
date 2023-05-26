using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FunionBlazor.Core.Entitys;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace FunionBlazor.Web.Core;
public class CPZWorker : BackgroundService
{
    // 服务工厂
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _configuration;
    public CPZWorker(IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string FilePath = _configuration["CPZ:FilePath"];
        string BackupFilePath = _configuration["CPZ:BackupFilePath"];
        using var scope = _scopeFactory.CreateScope();
        var services = scope.ServiceProvider;
        while (!stoppingToken.IsCancellationRequested)
        {
            if (Directory.Exists(FilePath) && Directory.Exists(BackupFilePath))
            {
                string[] txtFiles = Directory.GetFiles(FilePath, "*.txt");
                foreach (string file in txtFiles)
                {
                    //string content = File.ReadAllText(file, Encoding.GetEncoding("GB2312"));
                    //Console.WriteLine(content);
                    string[] lines = File.ReadAllLines(file, Encoding.GetEncoding("GB2312"));
                    SaveData(services, lines);
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(BackupFilePath, fileName);
                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                        File.Move(file, destFile);
                    }
                    else
                    {
                        File.Move(file, destFile);
                    }
                }
            }
            // 延迟 3 秒
            await Task.Delay(1000 * 3, stoppingToken);
        }
    }
    protected void SaveData(IServiceProvider services, string[] lines)
    {
        var CPZrespository = Db.GetRepository<OUnbalancedLoading>(services);
        var CDHrespository = Db.GetRepository<TrackScale>(services);
        var sysrespository = Db.GetRepository<SysSettings>(services);
        var cigs = sysrespository.Where(u => u.CPZOrder != 0).OrderBy(o => o.CPZOrder).ToList();
        var oUnbalanceds = new List<OUnbalancedLoading>();
        string CPZformat = _configuration["CPZ:CPZformat"];
        if (!int.TryParse(CPZformat, out int result))
        {
            result = 6;
        }
        string Direction = "";
        string CreateDatestr = "";
        DateTime CreateDate = DateTime.Now;
        for (int i = 0; i < lines.Length; i++)
        {
            //方向
            if (i == 1)
            {
                Direction = lines[i].Split(":")?[1];
            }
            //日期时间
            if (i == 3)
            {
                CreateDatestr = lines[i].Split(":")?[1];
                DateTime.TryParseExact(CreateDatestr, "yyMMdd_HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out CreateDate);
            }
            if (i >= (result - 1))
            {
                var arr = lines[i].Split(",");
                OUnbalancedLoading t = new()
                {
                    Id = Guid.NewGuid()
                };
                for (int j = 1; j < arr.Length; j++)
                {
                    if (j < cigs.Count)
                    {
                        typeof(OUnbalancedLoading).GetProperty(cigs[j - 1].AttributeName).SetValue(t, arr[j].Trim());
                    }
                }
                t.Direction = Direction;
                t.CreateDate = CreateDate;
                t.CreateDatestr = CreateDatestr;
                oUnbalanceds.Add(t);
            }
        }
        if (oUnbalanceds.Any())
        {
            var ch = oUnbalanceds.Select(x => x.WagonNumber).ToList();
            DateTime starDate = CreateDate.AddHours(-5);
            DateTime endDate = CreateDate.AddHours(5);
            //匹配
            var list = CDHrespository.Where(x => ch.Contains(x.WagonNumber))
                          .Where(u => u.CreateDate >= starDate && u.CreateDate <= endDate).ToList();
            oUnbalanceds.ForEach(item =>
            {
                var track = list.Where(o => o.WagonNumber == item.WagonNumber)
                                       .OrderByDescending(o => o.CreateDate)
                                       .FirstOrDefault();
                if (track != null)
                {
                    item.TrackScaleid = track.Id;
                }
            });
            CPZrespository.Context.BulkInsert(oUnbalanceds);
        }
    }

}


