using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caalinder.Models
{
    public class HorseViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nome do Cavalo")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Sexo")]
        public GenderEnum Gender { get; set; }
        [Required]
        [Display(Name = "Raça")]
        public string HorseBrand { get; set; }
        [UIHint("Date")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime HorseBirth { get; set; }
        [Required]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

    }
    public enum GenderEnum
    {
        Macho,
        Fêmea
    }

    public class HorseViewIndex
    {
        public IEnumerable<HorseViewModel> HorseViewsList { get; set; }
    }
}