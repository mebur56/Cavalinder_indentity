using Caalinder.Controllers.Interfaces;
using Caalinder.Data.Context;
using Caalinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Caalinder.Data.Repositories
{
    public class MatchRepository : GenericRepository<MatchModel>, IMatchRepository
    {
        private ApplicationContext _context;
        public MatchRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}