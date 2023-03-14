using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es_Arboles
{
    public class ABB <T> : NoLinear<T> where T : IComparable<T>
    {
        private Node<T> Raiz = new Node<T>();
        private Node<T> temp = new Node<T>();
        private List<T> listaOrdenada = new List<T>();
        public int comparaciones = 0;

        public Node<T> CrearNodoABB(T valor)
        {
            Node<T> nodo = new Node<T>();
            nodo.Valor = valor;
            nodo.Profundidad = 0;
            nodo.Izquierdo = new Node<T>();
            nodo.Derecho = new Node<T>();
            return nodo;

        }
        public void Add(T value)
        {
            Insert(value);
        }
        public void Insert(T value)
        {
            try
            {
                Node<T> nuevo = CrearNodoABB(value);

                if (Raiz.Valor == null)
                {
                    Raiz = nuevo;
                }
                else
                {
                    Raiz = InsertarABB(nuevo, Raiz);
                }
            }
            catch
            {
                throw;
            }
        }
        protected override Node<T> Insert(Node<T> nodo, T value)
        {
            try
            {
                Node<T> nuevo = CrearNodoABB(value);

                if (nodo == null)
                {
                    nodo = nuevo;
                }
                else
                {
                    nodo = InsertarABB(nuevo, nodo);

                }
                return nodo;

            }
            catch
            {
                throw;
            }
        }
        public Node<T> InsertarABB(Node<T> nodo, Node<T> tempo) //Se inserta el valor en el arbol y se verifico si está ordenado
        {
            try
            {
                Node<T> nuevoNodo = tempo;

                if (nodo.Valor.CompareTo(tempo.Valor) == -1)
                {
                    if (tempo.Izquierdo.Valor == null)
                    {
                        tempo.Izquierdo = nodo;
                    }
                    else
                    {
                        tempo.Izquierdo = InsertarABB(nodo, tempo.Izquierdo);
                    }
                }
                else if (nodo.Valor.CompareTo(tempo.Valor) == 1)
                {
                    if (tempo.Derecho.Valor == null)
                    {
                        tempo.Derecho = nodo;
                    }
                    else
                    {
                        tempo.Derecho = InsertarABB(nodo, tempo.Derecho);                        
                    }
                }                
                return nuevoNodo;
            }
            catch
            {
                throw;
            }
        }
        protected override void Delete(Node<T> nodo)
        {
            if (nodo.Izquierdo.Valor == null && nodo.Derecho.Valor == null) // Caso 1
            {
                nodo.Valor = nodo.Derecho.Valor;
            }
            else if (nodo.Derecho.Valor == null) // Caso 2
            {
                nodo.Valor = nodo.Izquierdo.Valor;
                nodo.Derecho = nodo.Izquierdo.Derecho;
                nodo.Izquierdo = nodo.Izquierdo.Izquierdo;
            }
            else // Caso 3
            {
                if (nodo.Izquierdo.Valor != null)
                {
                    temp = Derecha(nodo.Izquierdo);
                }
                else
                {
                    temp = Derecha(nodo);
                }
                nodo.Valor = temp.Valor;
            }

        }
        private Node<T> Derecha(Node<T> nodo)
        {
            if (nodo.Derecho.Valor == null)
            {
                if (nodo.Izquierdo.Valor != null)
                {
                    return Derecha(nodo.Izquierdo);
                }
                else
                {
                    Node<T> temporal = new Node<T>();
                    temporal.Valor = nodo.Valor;
                    nodo.Valor = nodo.Derecho.Valor;
                    return temporal;
                }
            }
            else
            {
                return Derecha(nodo.Derecho);
            }
        }
        public T Remove(T deleted)
        {
            Node<T> busc = new Node<T>();
            busc = Get(Raiz, deleted);
            if (busc != null)
            {
                Delete(busc);
            }
            return deleted;
        }
        protected override Node<T> Get(Node<T> nodo, T value)
        {
            if (value.CompareTo(nodo.Valor) == 0)
            {
                return nodo;
            }
            else if (value.CompareTo(nodo.Valor) == -1)
            {
                if (nodo.Izquierdo.Valor == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.Izquierdo, value);
                }
            }
            else
            {
                if (nodo.Derecho.Valor == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.Derecho, value);
                }
            }
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
        public List<T> ObtenerLista()
        {
            listaOrdenada.Clear();
            InOrder(Raiz);
            return listaOrdenada;
        }
        public List<T> Obtener(Func<T, bool> Predicate)
        {
            List<T> prov = new List<T>();
            comparaciones = 0;
            ObtenerLista();
            for (int i = 0; i < listaOrdenada.Count(); i++)
            {
                if (Predicate(listaOrdenada[i]))
                {
                    comparaciones = i;
                    prov.Add(listaOrdenada[i]);
                }
            }
            return prov;
        }
        public int GetComparaciones()
        {
            return comparaciones;
        }
        public int ObtenerProfundidad()
        {
            return ObtenerProfundidad(Raiz, 0, 0);
        }

        private int ObtenerProfundidad(Node<T> nodo, int profundidadActual, int profundidadMaxima)
        {
            if (nodo.Valor == null)
            {
                return profundidadMaxima;
            }

            // Incrementar la profundidad actual
            profundidadActual++;

            // Actualizar la profundidad máxima si la actual es mayor
            if (profundidadActual > profundidadMaxima)
            {
                profundidadMaxima = profundidadActual;
            }

            // Recorrer los nodos hijos
            profundidadMaxima = ObtenerProfundidad(nodo.Izquierdo, profundidadActual, profundidadMaxima);
            profundidadMaxima = ObtenerProfundidad(nodo.Derecho, profundidadActual, profundidadMaxima);

            return profundidadMaxima;
        }
    }
}
