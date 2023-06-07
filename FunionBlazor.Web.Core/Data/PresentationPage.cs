using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunionBlazor.Web.Core.Data
{
    public class PresentationPage : DataPage<PresentationDataDto>
    {

        public string CreateDatestr { get; set; }
        public string Cstr { get; set; }
        public List<FiltersDateDto> DateDtolist { get; set; }
        public PresentationPage(IQueryable<PresentationDataDto> datas, string createDate) : base(datas)
        {
            CreateDatestr = createDate;
        }
        public override IEnumerable<PresentationDataDto> GetFilterDatas()
        {
            IEnumerable<PresentationDataDto> datas = base.Datas;
            if (CreateDatestr is not null)
            {
                Cstr = CreateDatestr;
                datas = datas.Where(o => o.CreateDatestr == CreateDatestr);
            }
            return datas.OrderBy(o=>o.Sequence);
        }
    }
}
