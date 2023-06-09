using System.Collections.Generic;
using System.Threading.Tasks;

using BlazorComponent;

namespace FunionBlazor.Web.Core.Data
{
    public class PresentationPage : DataPage<PresentationDataDto>
    {

        public string CreateDatestr { get; set; }
        public string Cstr { get; set; }
        public List<FiltersDateDto> DateDtolist { get; set; }
        public PresentationPage(IQueryable<PresentationDataDto> datas) : base(datas)
        {
           
        }
        public override IEnumerable<PresentationDataDto> GetFilterDatas()
        {
            var datas = Datas;
            if (CreateDatestr is not null)
            {
                Cstr = CreateDatestr;
                datas = datas.Where(o => o.CreateDatestr == CreateDatestr);
            }
            var list = datas.AsEnumerable().OrderBy(o => o.Sequence);
            return list;
        }
        public override List<PresentationDataDto> GetPageDatas()
        {
            if (CurrentCount < (PageIndex - 1) * PageSize) PageIndex = 1;
            var list = GetFilterDatas().Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            list.ForEach(o =>
            {
                o.Suttle = (double.Parse(o.RoughWeight) - double.Parse(o.Tare)).ToString("F2");
                o.ProfitLoss = (double.Parse(o.Suttle) - double.Parse(o.IndicatedDeight)).ToString("F2");
            });
            return list;
        }
    }
}
