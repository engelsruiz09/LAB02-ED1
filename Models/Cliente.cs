using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LAB02_ED1_G.Models
{
    public class Cliente
    {
        [Display(Name = "ID")]
        [Required]
        public int? ID { get; set; }

        [Display(Name = "NumSerie")]
        [Required]
        public string NumSerie { get; set; }
    }
}
