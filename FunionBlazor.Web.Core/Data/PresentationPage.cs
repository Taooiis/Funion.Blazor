namespace FunionBlazor.Web.Core.Data
{
    public class PresentationPage : DataPage<PresentationDataDto>
    {
       
        public string CreateDatestr { get; set; }

        public PresentationPage(IQueryable<PresentationDataDto> datas,string createDate) : base(datas)
        {
            CreateDatestr = createDate;
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
