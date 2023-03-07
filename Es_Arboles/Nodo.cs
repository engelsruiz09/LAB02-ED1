using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es_Arboles
{
    public class Nodo<T>
    {
        public T valor { get; set; }
        public Nodo<T> Izquierda { get; set; }
        public Nodo<T> Derecha { get; set; }

        public int Profundidad { get; set; }
        public int ContadorComparaciones { get; set; }
    }
}
