using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LAB02_ED1_G.Models
{
    public class ExtensionVehiculo: ModeloVehiculo
    {
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Propietario")]
        [Required]
        public string Propietario { get; set; }
        [Display(Name = "Color")]
        [Required]
        public string Color { get; set; }
        [Display(Name = "Marca")]
        [Required]
        public string Marca { get; set; }

    }
}
