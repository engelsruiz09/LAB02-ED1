using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es_Arboles
{
    public class ArbolBinario<T> where T : IComparable<T>
    {
        private Node<T> Raiz = new Node<T>();
        private Node<T> temp = new Node<T>();
        private List<T> listaOrdenada = new List<T>();
        public int comparaciones = 0;

        public List<T> ObtenerLista()
        {
            listaOrdenada.Clear();
            InOrder(Raiz);
            return listaOrdenada;
        }

        private void InOrder(Node<T> nodo)
        {
            if (nodo.Valor != null)
            {
                InOrder(nodo.Izquierdo);
                listaOrdenada.Add(nodo.Valor);
                InOrder(nodo.Derecho);
            }
        }

    }
}
