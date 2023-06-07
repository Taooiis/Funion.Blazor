using Append.Blazor.Printing;
namespace FunionBlazor.Web.Entry.Pages
{
    public partial class Index
    {
        [Inject]
        private TrackScaleService TrackScaleService { get; set; }
        [Inject]
        private IPrintingService PrintingService { get; set; }
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        private IJSObjectReference? module;

        public IQueryable<PresentationDataDto> Datas;
        public PresentationPage _dataPage = null;

        //private List<string> Yearlist { get; set; }
        //private List<string> Monthlist { get; set; }
        //private List<string> Daylist { get; set; }
       
        private List<string> CreateDatestrlist { get; set; }
        private readonly List<int> _pageSizes = new() { 10, 25, 50, 100 };

      
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                module = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                    "./Pages/Index.razor.js");
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadListDataAsync();
        }
        public async Task RefterData()
        {
            await LoadListDataAsync();
        }
        private async Task PrintTable()
        {
            if (module is not null)
            {
               await module.InvokeAsync<string>("SetTableByfatherId", "MasaTable");
            }
            await PrintingService.Print("MDataTable", PrintType.Html);
        }
        private async Task LoadListDataAsync()
        {
            var datas = TrackScaleService.GetPresentationDataDtoList();
            string cstr = datas.OrderByDescending(o=>o.CreateDate).FirstOrDefault()?.CreateDatestr;
            _dataPage = new PresentationPage(datas, createDate: cstr);
            _dataPage.DateDtolist = TrackScaleService.GetTrackScaleDateDto();
            //CreateDatestrlist = _dataPage.Datas.Select(o=>o.CreateDatestr).Distinct().ToList();
        }

        private readonly List<DataTableHeader<PresentationDataDto>> _headers = new()
        {
            new() { Text = "序号", Value = nameof(PresentationDataDto.Sequence) },
            //new() { Text = "时间", Value = nameof(PresentationDataDto.CreateDatestr) },
            //new() { Text = "轨道衡测点", Value = nameof(PresentationDataDto.GdhStation) },
            new() { Text = "车号", Value = nameof(PresentationDataDto.WagonNumber) },
            new() { Text = "车型", Value = nameof(PresentationDataDto.MotorcycleType) },
            new() { Text = "毛重", Value = nameof(PresentationDataDto.RoughWeight) },
            new() { Text = "皮重", Value = nameof(PresentationDataDto.Tare) },
            new() { Text = "净重", Value = nameof(PresentationDataDto.Suttle) },
            new() { Text = "标重", Value = nameof(PresentationDataDto.IndicatedDeight) },
            new() { Text = "盈亏", Value = nameof(PresentationDataDto.ProfitLoss) },
            new() { Text = "速度", Value = nameof(PresentationDataDto.Speed) },
            //new() { Text = "货名", Value = nameof(PresentationDataDto.CargoName) },
            new() { Text = "发货单位", Value = nameof(PresentationDataDto.TransceiverSend) },
            //new() { Text = "收货单位", Value = nameof(PresentationDataDto.TransceiverCollect) },
            new() { Text = "发站", Value = nameof(PresentationDataDto.SendSend) },
            //new() { Text = "到站", Value = nameof(PresentationDataDto.SendCollect) },
            //new() { Text = "发站时间", Value = nameof(PresentationDataDto.StarCreateDatestr) },
            //new() { Text = "备注", Value = nameof(PresentationDataDto.Remark) },
            new() { Text = "是否匹配", Value = nameof(PresentationDataDto.IsMate) },
        };
    }
}
