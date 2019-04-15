using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Models
{
    public class HorseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string HorseBrand { get; set; }

        public DateTime HorseBirth { get; set; }

        public string Description { get; set; }

        public virtual UserModel User { get; set; }
    }
}