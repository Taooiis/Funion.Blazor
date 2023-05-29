using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FunionBlazor.Application.Dto;
using FunionBlazor.Core.Entitys;

namespace FunionBlazor.Application
{
    public class Mapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TrackScale, PresentationDataDto>()
                .Map(dest => dest.Tare, src => src.OUlanced.RoughWeight,src=> src.OUlanced!=null)
                .Map(dest => dest.Tare, src => src.Tare, src => src.OUlanced == null)
                .Map(dest => dest.IsMate, src => src.OUlanced != null)
                .Map(dest => dest.Sequence, src => src.Sequence);
            

        }
    }
}
