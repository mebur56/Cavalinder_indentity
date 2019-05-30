using Caalinder.Controllers;
using Caalinder.Controllers.Interfaces;
using Caalinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Service
{
    public class HorseService : GenericService<HorseModel>, IHorseService
    {
        private readonly IHorseRepository _horseService;
        public HorseService(IHorseRepository repository) : base(repository)
        {
            _horseService = repository;
        }

    }
}