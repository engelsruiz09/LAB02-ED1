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
    public class PropietarioEmail : IComparer<ExtensionVehiculo>
    {
        public int Compare(ExtensionVehiculo p1, ExtensionVehiculo p2)
        {
            return string.Compare(p1.Email, p2.Email);
        }
    }
    public class PropietarioID : IComparer<ExtensionVehiculo>
    {
        public int Compare(ExtensionVehiculo p1, ExtensionVehiculo p2)
        {
            return string.Compare(p1.ID, p2.ID);
        }
    }
    public class VehiculoID : IComparer<ExtensionVehiculo>
    {
        public int Compare(ExtensionVehiculo p1, ExtensionVehiculo p2)
        {
            int n1 = Convert.ToInt32(p1.NumSerie);
            int n2 = Convert.ToInt32(p2.NumSerie);
            return n1.CompareTo(n2);
        }
    }
}
