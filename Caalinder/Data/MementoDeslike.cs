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
            if (match != null)
            {
                this.match = new MatchModel
                {
                    ApplicationUser1 = match.ApplicationUser1,
                    ApplicationUser2 = match.ApplicationUser2,
                    Horse1Id = match.Horse1Id,
                    Horse2Id = match.Horse2Id,
                    Id = match.Id,
                    Match = match.Match,
                    Like1 = match.Like1,
                    Like2 = match.Like2
                };
            }
            else
            {
                this.match = new MatchModel
                {
                    ApplicationUser1 = null,
                    ApplicationUser2 =null,
                    Horse1Id = 0,
                    Horse2Id = 0,
                    Id = 0,
                    Match = false,
                    Like1 = false,
                    Like2 = false         
                };
            }
        }
        

        public MatchModel getState() 
        {
            return match;
        }
    }
}