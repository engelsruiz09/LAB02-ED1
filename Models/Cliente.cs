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
    
    //public class ClienteNombre: IComparable<Cliente>
    //{
    //    public int CompareTo(Cliente other)
    //    {
    //        if (other == null) return 0;
    //        else
    //        {
    //            return this.ID.CompareTo(other.ID);
    //        }
    //    }
    //}
}
