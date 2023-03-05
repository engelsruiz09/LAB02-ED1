using LAB02_ED1_G.Models.Datos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LAB02_ED1_G.Models
{
    public class ModeloVehiculo: IComparable<ModeloVehiculo>
    {
        [Display(Name = "ID")]
        [Required]
        public string ID { get; set; }
        [Display(Name = "NumSerie")]
        [Required]
        public string NumSerie { get; set; }

        //al definir la clase modelovehiculo como public se esta indicando que la clase modelovehiculo puede ser ordenada en base a un criterio especifico definido por el metodo compareTo , el metodo tiene que recibir un objeto como parametro y devuelve si el objeto actual es menor o igual o mayor que el objeto pasado como parametro.
        public int CompareTo(ModeloVehiculo other)
        {
            if (other == null) return 0;
            else
            {
                return this.NumSerie.CompareTo(other.NumSerie);
            }
        }

        public static List<ExtensionVehiculo> Filter(string Name)
        {
            return Singleton.Instance.ArbolVehiculos.ObtenerLista().Where(x => x.NumSerie.ToLower().Contains(Name.ToLower())).ToList();
        }
    }
}
