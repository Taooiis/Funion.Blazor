using System;

using FunionBlazor.Application.Dto;
using FunionBlazor.Core.Entitys;

using Furion.DatabaseAccessor.Extensions;

using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace FunionBlazor.Application
{
    public class TrackScaleService : IDynamicApiController, ITransient
    {
        private readonly IRepository<TrackScale> _repository;

        public TrackScaleService(IRepository<TrackScale> repository) => _repository = repository;

        public IQueryable<TrackScale> GetTrackScaleQueryable()
        {
            return _repository.AsQueryable().OrderByDescending(o => o.CreateDate).Include(u => u.OUlanced); ;
        }
        public List<DateDto> GetTrackScaleDateDto()
        {
            return "SELECT EXTRACT(YEAR FROM CreateDate) AS Year, EXTRACT(Month FROM CreateDate) AS Month,EXTRACT(DAY FROM CreateDate) AS DAY\r\nFROM (SELECT DISTINCT CreateDate FROM trackscale) as t".SqlQuery<DateDto>();
        }
        public IQueryable<PresentationDataDto> GetPresentationDataDtoList()
        {
            return _repository.AsQueryable().OrderByDescending(o => o.CreateDate).Include(u => u.OUlanced).ProjectToType<PresentationDataDto>();
        }
    }
}