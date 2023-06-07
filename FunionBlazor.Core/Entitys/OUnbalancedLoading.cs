using System;

using Furion.DatabaseAccessor;

namespace FunionBlazor.Core.Entitys
{
    /// <summary>
    /// 超偏载
    /// </summary>
    public class OUnbalancedLoading : Entity<Guid>
    {

        /// <summary>
        ///  GDHid
        /// </summary>
        public Guid? TrackScaleid { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 创建时间str
        /// </summary>
        public string CreateDatestr { get; set; }

        /// <summary>
        /// 轨道衡测点
        /// </summary>
        public string GdhStation { get; set; }

        /// <summary>
        /// 发站时间
        /// </summary>
        public string StarCreateDatestr { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        public string WagonNumber { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string MotorcycleType { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        public string RoughWeight { get; set; }

        /// <summary>
        /// 皮重
        /// </summary>
        public string Tare { get; set; }

        /// <summary>
        /// 净重
        /// </summary>
        public string Suttle { get; set; }

        /// <summary>
        /// 标重
        /// </summary>
        public string IndicatedDeight { get; set; }

        /// <summary>
        /// 盈亏
        /// </summary>
        public string ProfitLoss { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public string Speed { get; set; }

        /// <summary>
        /// 货名
        /// </summary>
        public string CargoName { get; set; }

        /// <summary>
        /// 发货单位
        /// </summary>
        public string TransceiverSend { get; set; }

        /// <summary>
        /// 收货单位
        /// </summary>
        public string TransceiverCollect { get; set; }

        /// <summary>
        /// 发站
        /// </summary>
        public string SendSend { get; set; }

        /// <summary>
        /// 到站
        /// </summary>
        public string SendCollect { get; set; }

        /// <summary>
        /// 前架
        /// </summary>
        public string SideArm { get; set; }

        /// <summary>
        /// 后架
        /// </summary>
        public string AfterFrame { get; set; }

        /// <summary>
        /// 前后差
        /// </summary>
        public string BeforeAfterPartial { get; set; }

        /// <summary>
        /// 重心偏
        /// </summary>
        public string CenterGravityPartial { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// 主表
        /// </summary>
        public TrackScale trackScale { get; set; }

    }
}
