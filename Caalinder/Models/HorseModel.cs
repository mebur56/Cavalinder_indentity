using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Caalinder.Models
{
    public class HorseModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string HorseBrand { get; set; }

        public DateTime HorseBirth { get; set; }

        public string Description { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        
        public string ApplicationUserID { get; set; }
    }
}