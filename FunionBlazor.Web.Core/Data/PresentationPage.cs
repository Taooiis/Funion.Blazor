using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunionBlazor.Web.Core.Data
{
    public class PresentationPage : DataPage<PresentationDataDto>
    {

        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string CreateDatestr { get; set; }
        public List<DateDto> DateDtolist { get; set; }
        public PresentationPage(IQueryable<PresentationDataDto> datas, string createDate) : base(datas)
        {
            CreateDatestr = createDate;
        }
        public List<string> GetYearList()
        {
            return DateDtolist.Select(o => o.Year).Distinct().ToList();
        }
        public List<string> GetMonthList()
        {
            List<string> year = new();
            if (Year is not null)
            {
                year = DateDtolist.Where(o => o.Year == Year).Select(o => o.Month).Distinct().ToList();
            }
            return year;
        }
        public List<string> GetDayList()
        {
            List<string> day = new();
            if (Year is not null&& day is not null)
            {
                day = DateDtolist.Where(o => o.Year == Year&&o.Month== Month).Select(o => o.Day).Distinct().ToList();
            }
            return day;
        }
        public async Task GetCreateDatestrlist()
        {

        }
        public override IEnumerable<PresentationDataDto> GetFilterDatas()
        {
            IEnumerable<PresentationDataDto> datas = base.Datas;
            if (CreateDatestr is not null)
            {
                datas = datas.Where(o => o.CreateDatestr == CreateDatestr);
            }
            return datas.OrderBy(o=>o.Sequence);
        }
    }
}
