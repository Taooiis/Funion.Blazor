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
        public List<FiltersDateDto> GetTrackScaleDateDto()
        {
            return "SELECT EXTRACT(YEAR FROM CreateDate) AS Year, EXTRACT(Month FROM CreateDate) AS Month,EXTRACT(DAY FROM CreateDate) AS DAY,CreateDatestr\r\nFROM (SELECT DISTINCT CreateDate,CreateDatestr FROM trackscale ORDER BY  CreateDate DESC) as t;".SqlQuery<FiltersDateDto>();
        }
        public IQueryable<PresentationDataDto> GetPresentationDataDtoList()
        {
            var list= _repository.AsQueryable().OrderByDescending(o => o.CreateDate).Include(u => u.OUlanced)
                .ProjectToType<PresentationDataDto>();
            return list;
        }
    }
}