using Caalinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(
                cfg =>
                {

                    #region  Horse 
                    cfg.CreateMap<HorseModel, HorseViewModel>().ReverseMap();
                    #endregion
                }
                );
        }
    }
}