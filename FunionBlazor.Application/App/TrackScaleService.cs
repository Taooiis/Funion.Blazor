using FunionBlazor.Application.Dto;
using FunionBlazor.Core.Entitys;
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
        public List<TrackScale> GetTrackScaleList()
        {
            return _repository.Include(u => u.OUlanced).ToList(); 
        }
        public IQueryable<PresentationDataDto> GetPresentationDataDtoList()
        {
            return _repository.AsQueryable().OrderByDescending(o => o.CreateDate).Include(u => u.OUlanced).ProjectToType<PresentationDataDto>();
        }
    }
}