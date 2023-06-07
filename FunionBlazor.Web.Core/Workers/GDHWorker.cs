using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FunionBlazor.Core.Entitys;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FunionBlazor.Web.Core;
public class GDHWorker : BackgroundService
{
    // 服务工厂
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _configuration;
    private static List<SysSettings> sysSettings = new List<SysSettings>();
    public GDHWorker(IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        using var scope = _scopeFactory.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        var sysrespository = Db.GetRepository<SysSettings>(services);
        sysSettings = sysrespository.Where(u => u.GDHOrder != 0).OrderBy(o => o.GDHOrder).ToList();
        string FilePath = _configuration["GDH:FilePath"];
        string BackupFilePath = _configuration["GDH:BackupFilePath"];
        if (!Directory.Exists(FilePath))
        {
            Directory.CreateDirectory(FilePath);
        }

        if (!Directory.Exists(BackupFilePath))
        {
            Directory.CreateDirectory(BackupFilePath);
        }
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
                    else {
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
        var GDHrespository = Db.GetRepository<TrackScale>(services);
        List<TrackScale> tracks = new();
        string GDHformat = _configuration["GDH:GDHformat"];
        if (!int.TryParse(GDHformat, out int result))
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
                TrackScale t = new()
                {
                    Id = Guid.NewGuid(),
                    Sequence = arr[0].Trim()
                };
                for (int j = 1; j < arr.Length; j++)
                {
                    if (j < sysSettings.Count)
                    {
                        typeof(TrackScale).GetProperty(sysSettings[j-1].AttributeName).SetValue(t, arr[j].Trim());
                    }
                }
                t.Direction= Direction;
                t.CreateDate = CreateDate;
                t.CreateDatestr = CreateDatestr;
                tracks.Add(t);
            }
        }
        if (tracks.Any()) {
            GDHrespository.Context.BulkInsert(tracks);
        }
    }

}


