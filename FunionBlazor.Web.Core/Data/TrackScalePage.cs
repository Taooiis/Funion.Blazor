using System.Collections.Generic;
using System.Threading.Tasks;

using BlazorComponent;

namespace FunionBlazor.Web.Core.Data
{
    public class TrackScalePage : DataPage<TrackScaleDataDto>
    {

        public string CreateDatestr { get; set; }
        public string Cstr { get; set; }
        public List<FiltersDateDto> DateDtolist { get; set; }
        public TrackScalePage(IQueryable<TrackScaleDataDto> datas) : base(datas)
        {
           
        }
        public override IEnumerable<TrackScaleDataDto> GetFilterDatas()
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
        public override List<TrackScaleDataDto> GetPageDatas()
        {
            if (CurrentCount < (PageIndex - 1) * PageSize) PageIndex = 1;
            var list = GetFilterDatas().Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            return list;
        }
    }
}
