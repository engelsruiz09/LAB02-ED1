using System;
using System.Collections.Generic;

namespace Es_Arboles
{
    public class ArbolBinario<T> : Node<T> where T : IComparable<T>
    {
        Node<T> NodoPadre = new Node<T>();
        Node<T> NodoBorrar = new Node<T>();
        public int AuxProfundidadIz = 0;
        public int AuxProfundidadDe = 0;

        public void Agregar(T valor)
        {
            Insercion(NodoPadre, valor); // Funcion que inserta el elemento
        }

        public void Borrar(T valor)
        {
            Eliminacion(NodoPadre, valor); // Funcion que elimina el elemento
        }

        public void Insercion(Node<T> padre, T valor)
        {
            //Si el nodo esta vacio entonces se insertara en ese lugar, si no se mueve
            if(padre.Valor == null)
            {
                padre.Valor= valor; // Inserta el elemento

                //Inicializa los hijos del nodo insertado
                padre.Izquierdo = new Node<T>();
                padre.Derecho = new Node<T>();
            }
            else if(valor.CompareTo(padre.Valor)== -1) //(valor < padre.valor)//Verfica si se tiene que mover a la izquierda, si no se mueve a la derecha
            {
                Insercion(padre.Izquierdo, valor ); //Se mueve a la izquierda
            }
            else if(valor.CompareTo(padre.Valor)== 1) //(valor > padre.valor)
            {
                Insercion(padre.Derecho, valor ); // Se mueve a la derecha
            }
        }

        public List<T> Recorrido()
        {
            Profundidad = 0;
            List<T> Listarbol= new List<T>();
            R(NodoPadre, Listarbol);
            Profundidad++;
            return Listarbol;
        }

        private void R(Node<T> padre, List<T> Lista)
        {
            if(padre.Valor!= null)
            {
                if(AuxProfundidadIz > Profundidad)
                {
                    Profundidad++;
                }
                if(AuxProfundidadDe > Profundidad)
                {
                    Profundidad++;
                }
                AuxProfundidadIz++;

                R(padre.Izquierdo, Lista);
                Lista.Add(padre.Valor);

                AuxProfundidadIz = 0;
                AuxProfundidadDe++;

                R(padre.Derecho, Lista);

                AuxProfundidadDe = 0;
            }
        }

        public T Busqueda(T valor)
        {
            Node<T> aux = NodoPadre;
            Node<T> noexiste = new Node<T>();
            while(aux.Valor!=null)
            {
                if(valor.CompareTo(aux.Valor) == 0) //(valor == aux.valor)//Encontro el valor buscado
                {
                    ContadorComparaciones += 1;
                    return aux.Valor; // Devuelve el valor buscado
                }
                else if(valor.CompareTo(aux.Valor)== -1)//(valor < aux.valor)//Compara para saber si se tiene que mover a la izquierda
                {
                    if(aux.Izquierdo.Valor!=null) //Verifica que no se encuentre en una hoja
                    {
                        ContadorComparaciones += 1;
                        aux = aux.Izquierdo;
                    }
                    else // Si se encuentra en una hoja, entonces no existe el valor buscado
                    {
                        return noexiste.Valor; // Devuelve un valor vacio porque no encontro lo solicitado
                    }
                }
                else if(valor.CompareTo(aux.Valor) == 1) //(valor > aux.valor)//Compara para saber si se tiene que mover a la derecha
                {
                    if(aux.Derecho.Valor!= null) // Verifica que no se encuentre en una hoja
                    {
                        ContadorComparaciones += 1;
                        aux = aux.Derecho; // Se mueve a la derecha
                    }
                    else // Si se encuentra en una hoja entonces no existe el valor buscado
                    {
                        return noexiste.Valor;
                    }
                }
            }
            return aux.Valor; // El arbol esta vacio
        }

        public void Eliminacion(Node<T> padre, T valor )
        {
            //Busca el elemento a eliminar con recursividad
            if(valor.CompareTo(padre.Valor) == -1) //(valor < padre.valor)
            {
                Eliminacion(padre.Izquierdo, valor );
            }
            else if(valor.CompareTo(padre.Valor) == 1) //(valor > padre.valor)
            {
                Eliminacion(padre.Derecho, valor );
            }
            else if(valor.CompareTo(padre.Valor) == 0) //(valor == padre.valor)//Encontro el elemento a eliminar
            {
                NodoBorrar = padre;
                Node<T> aux = new Node<T>();

                //Comprueba si tiene hijos
                if(padre.Izquierdo.Valor == null && padre.Derecho.Valor == null) //Si cumple entonces no tiene hijos, si no avanza y comprueba de que lado esta el hijo
                {
                    NodoBorrar = new Node<T>();
                    padre.Valor = NodoBorrar.Valor;
                }

                //Verifica si es el hijo izquierdo, si no entonces es el hijo derecho
                else if(padre.Derecho.Valor == null)
                {
                    padre = padre.Izquierdo; // Se mueve al sub-arbol izquierdo del nodo a eliminar

                    //Se busca la hoja con el valor a remplazar el valor eliminado
                    while(padre.Derecho.Valor!= null)
                    {
                        aux = padre; //Se guarda al padre del hijo a remplazar
                        padre = padre.Derecho; // Se guarda el valor mas grande de los menores
                    }
                    NodoBorrar.Valor = padre.Valor; // Se remplaza el nodo eliminado con el nuevo valor para mantener coherencia en el arbol
                    NodoBorrar = padre.Izquierdo; // El NodoBorrar se vuelve null
                    aux.Derecho = NodoBorrar; // Se borra la hoja ya que esta fue remplazada
                }
                //Entrara aqui por dos razones
                // 1. Solo tiene un hijo y es el derecho
                // 2. Tiene los dos hijos y se buscara el elemento mas a la izquierda del sub arbol derecho para remplazar al elemnto eliminado
                else 
                {
                    padre = padre.Derecho; // Se mueve al sub-arbol derecho del nodo a eliminar

                    // Se busca la hoja con el valor con el que se va a remplazar el valor eliminado
                    while(padre.Izquierdo.Valor != null)
                    {
                        aux = padre; // Se guarda al padre del hijo que va a remplazar
                        padre = padre.Izquierdo; // Se guarda e; valor mas pequeno del lado derecho
                    }
                    NodoBorrar.Valor = padre.Valor; // Se remplaza  el nodo eliminado con el nuevo valor para mantener coherencia en el arbol
                    NodoBorrar = padre.Derecho; // El NodoBorrar se vuelve null
                    aux.Izquierdo = NodoBorrar; // Se borra la hoja ya que esta fue remplazada
                }
            }
        }
    }

  /*  public class ArbolBinario<T> where T : IComparable<T>
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

    }*/
}
