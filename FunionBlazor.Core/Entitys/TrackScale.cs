using System;

using Furion.DatabaseAccessor;

namespace FunionBlazor.Core.Entitys
{
    /// <summary>
    /// 轨道衡
    /// </summary>
    public class TrackScale : Entity<Guid>
    {

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 创建时间str
        /// </summary>
        public string CreateDatestr { get; set; }

        /// <summary>
        /// 发站时间
        /// </summary>
        public string StarCreateDatestr { get; set; }

        /// <summary>
        /// 轨道衡测点
        /// </summary>
        public string GdhStation { get; set; }

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
        /// 超吨
        /// </summary>
        public string SuperTons { get; set; }

        /// <summary>
        /// 是否匹配 1是已匹配 0是未匹配
        /// </summary>
        public string OverT { get; set; }

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
        /// 船名
        /// </summary>
        public string ShipsName { get; set; }

        /// <summary>
        /// 装车点
        /// </summary>
        public string LoadingName { get; set; }

        /// <summary>
        /// 允增
        /// </summary>
        public string WeightOff { get; set; }

        /// <summary>
        /// 前后偏
        /// </summary>
        public string BeforeAfterPartial { get; set; }

        /// <summary>
        /// 左右偏
        /// </summary>
        public string AboutPartial { get; set; }

        /// <summary>
        /// 整车偏
        /// </summary>
        public string VehiclePartial { get; set; }

        /// <summary>
        /// 左一（港次）
        /// </summary>
        public string LeftOne { get; set; }

        /// <summary>
        /// 右一（换长）
        /// </summary>
        public string RightOne { get; set; }

        /// <summary>
        /// 左二
        /// </summary>
        public string LeftTwo { get; set; }

        /// <summary>
        /// 右二
        /// </summary>
        public string RightTwo { get; set; }

        /// <summary>
        /// 左三
        /// </summary>
        public string LeftThree { get; set; }

        /// <summary>
        /// 右三
        /// </summary>
        public string RightThree { get; set; }

        /// <summary>
        /// 左四
        /// </summary>
        public string LeftFour { get; set; }

        /// <summary>
        /// 右四（车数）
        /// </summary>
        public string RightFour { get; set; }

        /// <summary>
        /// 备注：0-新数据 1-未完成数据 2-已完成数据 3-已交接数据 4-待匹配
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 结束时间str
        /// </summary>
        public string SendState { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string Sequence { get; set; }

        /// <summary>
        /// 总和
        /// </summary>
        public int Sum { get; set; }


        /// <summary>
        /// 从表
        /// </summary>
        public OUnbalancedLoading OUlanced { get; set; }

    }
}
