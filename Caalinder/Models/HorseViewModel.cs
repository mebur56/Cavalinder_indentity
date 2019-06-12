using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caalinder.Models
{
    public class HorseViewModel
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string HorseBrand { get; set; }

        [DataType(DataType.Date)]
        public DateTime HorseBirth { get; set; }

        public string Description { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }
    }
}