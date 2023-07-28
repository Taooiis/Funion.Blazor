namespace FunionBlazor.Web.Entry.Pages
{
    public partial class TrackScaleData
    {
        [Inject]
        private TrackScaleService TrackScaleService { get; set; }
        
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private IJSObjectReference? module;

        public IQueryable<TrackScaleDataDto> Datas;
        public TrackScalePage _dataPage = null;
        private List<BCascaderNode> _Nodes { get; set; }

        private readonly List<int> _pageSizes = new() { 10, 25, 50, 100 };
        public class BCascaderNode
        {
            public string Value { get; set; }
            public string Label { get; set; }
            public List<BCascaderNode> Children { get; set; } = new();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                module = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                    "./Pages/trackScaleData.razor.js");
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
                await module.InvokeAsync<string>("printContent", "MDataTable",_dataPage.Cstr);
            }
            await InvokeAsync(() => StateHasChanged());
        }


        private async Task LoadListDataAsync()
        {
            var list = TrackScaleService.GetTrackScaleDateDto();
            string cstr = list.FirstOrDefault().CreateDatestr;
            var datas = TrackScaleService.GetTrackScaleDataDtoList();
            _dataPage = new TrackScalePage(datas);
            _dataPage.DateDtolist = list;
            _dataPage.CreateDatestr = cstr;
            LoadChildren(_dataPage.DateDtolist);

        }
        public void LoadChildren(List<FiltersDateDto> list)
        {
            _Nodes = list.GroupBy(fd => fd.Year)
                        .Select(y => new BCascaderNode
                        {
                            Value = y.Key.ToString(),
                            Label = $"{y.Key}年",
                            Children = y.GroupBy(m => m.Month)
                            .Select(m => new BCascaderNode
                            {
                                Value = $"{y.Key}-{m.Key}",
                                Label = $"{m.Key}月",
                                Children = m.GroupBy(d => d.Day)
                                .Select(d => new BCascaderNode
                                {
                                    Value = $"{y.Key}-{m.Key}-{d.Key}",
                                    Label = $"{d.Key}日",
                                    Children = d.GroupBy(x => x.CreateDatestr)
                                    .Select(x => new BCascaderNode
                                    {
                                        Value = x.Key,
                                        Label = x.Key
                                    }).ToList()
                                }).ToList()
                            }).ToList()
                        }).ToList();
        }

        private readonly List<DataTableHeader<TrackScaleDataDto>> _headers = new()
        {
            new() { Text = "序号", Value = nameof(TrackScaleDataDto.Sequence) },
            //new() { Text = "时间", Value = nameof(TrackScaleDataDto.CreateDatestr) },
            //new() { Text = "轨道衡测点", Value = nameof(TrackScaleDataDto.GdhStation) },
            new() { Text = "车号", Value = nameof(TrackScaleDataDto.WagonNumber) },
            new() { Text = "车型", Value = nameof(TrackScaleDataDto.MotorcycleType) },
            new() { Text = "毛重", Value = nameof(TrackScaleDataDto.RoughWeight) },
            new() { Text = "皮重", Value = nameof(TrackScaleDataDto.Tare) },
            new() { Text = "净重", Value = nameof(TrackScaleDataDto.Suttle) },
            new() { Text = "标重", Value = nameof(TrackScaleDataDto.IndicatedDeight) },
            new() { Text = "盈亏", Value = nameof(TrackScaleDataDto.ProfitLoss) },
            //new() { Text = "速度", Value = nameof(TrackScaleDataDto.Speed) },
            //new() { Text = "货名", Value = nameof(TrackScaleDataDto.CargoName) },
            //new() { Text = "发货单位", Value = nameof(TrackScaleDataDto.TransceiverSend) },
            //new() { Text = "收货单位", Value = nameof(TrackScaleDataDto.TransceiverCollect) },
            //new() { Text = "发站", Value = nameof(TrackScaleDataDto.SendSend) },
            //new() { Text = "到站", Value = nameof(TrackScaleDataDto.SendCollect) },
            //new() { Text = "发站时间", Value = nameof(TrackScaleDataDto.StarCreateDatestr) },
            //new() { Text = "备注", Value = nameof(TrackScaleDataDto.Remark) },
        };
    }
}
