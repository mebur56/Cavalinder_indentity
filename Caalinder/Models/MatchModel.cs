using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Models
{
    public class MatchModel
    {
        public bool Match { get; set; }
        public bool Like1 { get; set; }
        public bool Like2 { get; set; }
        public int HorseId1 { get; set; }
        public int HorseId2 { get; set; }
    }
}