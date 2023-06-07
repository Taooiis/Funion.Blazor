using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunionBlazor.Application.Dto
{
    public class FiltersDateDto
    {
        /// <summary>
        /// 年
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// 日期时间
        /// </summary>
        public string CreateDatestr { get; set; }
        
    }
}
