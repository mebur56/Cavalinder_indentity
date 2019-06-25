using Caalinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Data
{
    public class MementoDeslike
    {
        private MatchModel match;

        public MementoDeslike(MatchModel match)
        {
            this.match = match;
        }

        public MatchModel getState()
        {
            return match;
        }
    }
}