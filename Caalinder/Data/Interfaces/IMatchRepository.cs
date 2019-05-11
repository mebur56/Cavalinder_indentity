using Caalinder.Models;
using Caalinder.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Controllers.Interfaces
{
    public interface IMatchRepository : IGenericRepository<MatchModel>
    {
    }
}