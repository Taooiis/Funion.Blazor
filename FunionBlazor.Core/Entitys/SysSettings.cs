using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Furion.DatabaseAccessor;

using Microsoft.EntityFrameworkCore;

namespace FunionBlazor.Core.Entitys
{
    /// <summary>
    /// 配置表
    /// </summary>
    public class SysSettings : Entity, IEntitySeedData<SysSettings>
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        /// CPZ顺序
        /// </summary>
        public int CPZOrder { get; set; }

        /// <summary>
        /// GDH顺序
        /// </summary>
        public int GDHOrder { get; set; }

        // 配置种子数据
        public IEnumerable<SysSettings> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<SysSettings>
            {
                new SysSettings { Id = 1, Name = "轨道衡测点",AttributeName="GDHStation",CPZOrder=0,GDHOrder=0},
                new SysSettings { Id = 2, Name = "车号",AttributeName="WagonNumber",CPZOrder=1,GDHOrder=1},
                new SysSettings { Id = 3, Name = "车型",AttributeName="MotorcycleType",CPZOrder=11,GDHOrder=11},
                new SysSettings { Id = 4, Name = "毛重", AttributeName="RoughWeight", CPZOrder=2,  GDHOrder=2 },
                new SysSettings { Id = 5, Name = "皮重", AttributeName="Tare", CPZOrder=3,  GDHOrder=3},
                new SysSettings { Id = 6, Name = "净重", AttributeName="Suttle", CPZOrder=4,  GDHOrder=4},
                new SysSettings { Id = 7, Name = "标重", AttributeName="IndicatedDeight", CPZOrder=8,  GDHOrder=8},
                new SysSettings { Id = 8, Name = "盈亏", AttributeName="ProfitLoss", CPZOrder=9,  GDHOrder=9},
                new SysSettings { Id = 9, Name = "超吨", AttributeName="SuperTons", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 10, Name = "速度", AttributeName="Speed", CPZOrder=10,  GDHOrder=10},
                new SysSettings { Id = 11, Name = "货名", AttributeName="CargoName", CPZOrder=5,  GDHOrder=5},
                new SysSettings { Id = 12, Name = "发货单位", AttributeName="TransceiverSend", CPZOrder=6,  GDHOrder=6},
                new SysSettings { Id = 13, Name = "收货单位", AttributeName="TransceiverCollect", CPZOrder=7,  GDHOrder=7},
                new SysSettings { Id = 14, Name = "发站", AttributeName="SendSend", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 15, Name = "到站", AttributeName="SendCollect", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 16, Name = "船名", AttributeName="ShipsName", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 17, Name = "装车点", AttributeName="LoadingName", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 18, Name = "允增", AttributeName="WeightOff", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 19, Name = "前后偏", AttributeName="BeforeAfterPartial", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 20, Name = "左右偏", AttributeName="AboutPartial", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 21, Name = "整车偏", AttributeName="VehiclePartial", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 22, Name = "左一", AttributeName="LeftOne",  CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 23, Name = "右一", AttributeName="RightOne", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 24, Name = "左二", AttributeName="LeftTwo", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 25, Name = "右二", AttributeName="RightTwo", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 26, Name = "左三", AttributeName="LeftThree", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 27, Name = "右三", AttributeName="RightThree", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 28, Name = "左四", AttributeName="LeftFour", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 29, Name = "右四", AttributeName="RightFour", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 30, Name = "方向", AttributeName="Direction", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 31, Name = "重心偏", AttributeName="CenterGravityPartial", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 32, Name = "前架", AttributeName="SideArm", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 33, Name = "整车偏", AttributeName="VehiclePartial", CPZOrder=0,  GDHOrder=0},
                new SysSettings { Id = 34, Name = "时间", AttributeName="CreateDatestr", CPZOrder=0,  GDHOrder=0},
            };
        }
    }
}
