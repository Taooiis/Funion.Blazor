using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlazorComponent.Web;
using EFCore.BulkExtensions;
using FunionBlazor.Core.Entitys;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FunionBlazor.Web.Core;
public class MateWorker : BackgroundService
{
    // 服务工厂
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _configuration;
    private static List<SysSettings> sysSettings = new List<SysSettings>();
    public MateWorker(IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var services = scope.ServiceProvider;
        var sysrespository = Db.GetRepository<SysSettings>(services);
        sysSettings = sysrespository.Where(u => u.GDHOrder != 0).OrderBy(o => o.GDHOrder).ToList();
        while (!stoppingToken.IsCancellationRequested)
        {
            MateData(services);
            // 延迟 3 秒
            await Task.Delay(1000 * 10, stoppingToken);
        }
    }
    protected void MateData(IServiceProvider services)
    {
        var CPZrespository = Db.GetRepository<OUnbalancedLoading>(services);
        var CDHrespository = Db.GetRepository<TrackScale>(services);
        var oUnbalanceds = CPZrespository.Where(x => x.TrackScaleid == null).ToList();
        if (oUnbalanceds.Any())
        {
            var ch = oUnbalanceds.Select(x => x.WagonNumber).ToList();
            //匹配
            List<TrackScale> list = CDHrespository.Where(x => ch.Contains(x.WagonNumber)).ToList();
            List<TrackScale> uplist = new();
            List<OUnbalancedLoading> upoulist = new();
            oUnbalanceds.ForEach(item =>
            {
                var track = list.Where(o => o.OverT != "1" && o.WagonNumber == item.WagonNumber && o.CreateDate < item.CreateDate)
                                       .OrderByDescending(o => o.CreateDate)
                                       .FirstOrDefault();
                if (track != null)
                {
                    item.TrackScaleid = track.Id;
                    track.OverT = "1";
                    uplist.Add(track);
                    upoulist.Add(item);
                }
            });
            if (upoulist.Any()) {
                CPZrespository.Context.BulkUpdate(upoulist);
            }
            if (uplist.Any())
            {
                CDHrespository.Context.BulkUpdate(uplist);
                GenerateMatchingText(uplist, upoulist);
            }
        }
    }

    private void GenerateMatchingText(List<TrackScale> uplist, List<OUnbalancedLoading> upoulist)
    {
        string sourcePath = _configuration["GDH:BackupFilePath"];
        string targetPath = _configuration["MateData:FilePath"];
        string format = _configuration["GDH:GDHformat"];
        if (!Directory.Exists(sourcePath))
        {
            Directory.CreateDirectory(sourcePath);
        }
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }
        List<string> CreadCsttxt = uplist.Select(o => o.CreateDatestr).Distinct().ToList();
        CreadCsttxt.ForEach(item =>
        {
            string fileName = $"{item}.txt";
            string sourceFilePath = Path.Combine(sourcePath, fileName);
            string targetFilePath = Path.Combine(targetPath, fileName);
            if (File.Exists(sourceFilePath))
            {
               string[] lines = File.ReadAllLines(sourceFilePath, Encoding.GetEncoding("GB2312"));
                if (!int.TryParse(format, out int result))
                {
                    result = 7;
                }
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i >= (result - 1))
                    {
                        var arr = lines[i].Split(",");
                        int chindex = sysSettings.FindIndex(o => o.AttributeName == "WagonNumber");
                        string ch = arr[chindex+1].Trim();
                        //皮重
                        int Tareindex = sysSettings.FindIndex(o => o.AttributeName == "Tare");
                        //毛重
                        int RoughWeightindex = sysSettings.FindIndex(o => o.AttributeName == "RoughWeight");
                        //净重
                        int Suttleindex = sysSettings.FindIndex(o => o.AttributeName == "Suttle");
                        //标重
                        int IndicatedDeightindex = sysSettings.FindIndex(o => o.AttributeName == "IndicatedDeight");
                        //盈亏
                        int ProfitLossindex = sysSettings.FindIndex(o => o.AttributeName == "ProfitLoss");
                        var ou = upoulist.FirstOrDefault(o => o.WagonNumber == ch);
                        if(ou != null) {
                            arr[Tareindex + 1] = ou.RoughWeight;
                            //毛重-皮重=净重
                            string Suttle = (double.Parse(arr[RoughWeightindex + 1].Trim()) - double.Parse(ou.RoughWeight)).ToString("F2");
                            arr[Suttleindex + 1] = Suttle;
                            //净重 - 标重 = 盈亏
                            string ProfitLoss= (double.Parse(Suttle) - double.Parse(arr[IndicatedDeightindex + 1].Trim())).ToString("F2");
                            arr[ProfitLossindex + 1] = Suttle; 
                        }
                        lines[i] = string.Join(",",arr);
                    }
                }
                File.WriteAllLines(targetFilePath, lines);
            }
        });
    }

}


