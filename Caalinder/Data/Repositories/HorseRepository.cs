using Caalinder.Controllers.Interfaces;
using Caalinder.Data.Context;
using Caalinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Data.Repositories
{
    public class HorseRepository : GenericRepository<HorseModel>, IHorseRepository
    {
        private ApplicationContext _context;
        public HorseRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}