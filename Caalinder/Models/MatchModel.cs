using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caalinder.Models
{
    public class MatchModel
    {
        [Key]
        public int Id { get; set; }
        public int Horse1Id { get; set; }
        public int Horse2Id { get; set; }
        public bool Like1 { get; set; }
        public bool Like2 { get; set; }
        public bool Match { get; set; }
        public string ApplicationUser1 { get; set; }
        public string ApplicationUser2 { get; set; }
    }
}