using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es_Arboles
{
    public class Node<T> 
    {
        public T Valor { get; set; }
        public Node<T> Izquierdo { get; set; }
        public Node<T> Derecho { get; set; }
        

        //Para el Binario
        public int Profundidad { get; set; }
        public int ContadorComparaciones { get; set; }

        //Para el AVL
        public int FE { get; set; }
        //FE = altura subarbol derecho - altura subarbolizquierdo por definicio para un arbol AVL este valor debe ser -1, 0 , 1 si el factor equilibrio de un nodo es 0 -> el nodo esta equilibrado y sus subarboles tienen exactamente la misma altura.
    }
}
