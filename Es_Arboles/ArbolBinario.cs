using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es_Arboles
{
    public class ArbolBinario<T> : Nodo<T> where T : IComparable
    {
        Nodo<T> NodoPadre = new Nodo<T>();
        Nodo<T> NodoBorrar = new Nodo<T>();
        public int AuxProfundidadIz = 0;
        public int AuxProfundidadDe = 0;

        public void Agregar(T valor, Delegate delegado)
        {
            Insercion(NodoPadre, valor, delegado); // Funcion que inserta el elemento
        }

        public void Borrar(T valor, Delegate delegado)
        {
            Eliminacion(NodoPadre, valor, delegado); // Funcion que elimina el elemento
        }

        public void Insercion(Nodo<T> padre, T valor, Delegate delegado)
        {
            //Si el nodo esta vacio entonces se insertara en ese lugar, si no se mueve
            if(padre.valor == null)
            {
                padre.valor= valor; // Inserta el elemento

                //Inicializa los hijos del nodo insertado
                padre.Izquierda = new Nodo<T>();
                padre.Derecha = new Nodo<T>();
            }
            else if(Convert.ToInt32(delegado.DynamicInvoke(valor, padre.valor))== -1) //(valor < padre.valor)//Verfica si se tiene que mover a la izquierda, si no se mueve a la derecha
            {
                Insercion(padre.Izquierda, valor, delegado); //Se mueve a la izquierda
            }
            else if(Convert.ToInt32(delegado.DynamicInvoke(valor, padre.valor))== 1) //(valor > padre.valor)
            {
                Insercion(padre.Derecha, valor, delegado); // Se mueve a la derecha
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

        private void R(Nodo<T> padre, List<T> Lista)
        {
            if(padre.valor!= null)
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

                R(padre.Izquierda, Lista);
                Lista.Add(padre.valor);

                AuxProfundidadIz = 0;
                AuxProfundidadDe++;

                R(padre.Derecha, Lista);

                AuxProfundidadDe = 0;
            }
        }

        public T Busqueda(T valor, Delegate delegado)
        {
            Nodo<T> aux = NodoPadre;
            Nodo<T> noexiste = new Nodo<T>();
            while(aux.valor!=null)
            {
                if(Convert.ToInt32(delegado.DynamicInvoke(valor, aux.valor)) == 0) //(valor == aux.valor)//Encontro el valor buscado
                {
                    ContadorComparaciones += 1;
                    return aux.valor; // Devuelve el valor buscado
                }
                else if(Convert.ToInt32(delegado.DynamicInvoke(valor, aux.valor))== -1)//(valor < aux.valor)//Compara para saber si se tiene que mover a la izquierda
                {
                    if(aux.Izquierda.valor!=null) //Verifica que no se encuentre en una hoja
                    {
                        ContadorComparaciones += 1;
                        aux = aux.Izquierda;
                    }
                    else // Si se encuentra en una hoja, entonces no existe el valor buscado
                    {
                        return noexiste.valor; // Devuelve un valor vacio porque no encontro lo solicitado
                    }
                }
                else if(Convert.ToInt32(delegado.DynamicInvoke(valor, aux.valor)) == 1) //(valor > aux.valor)//Compara para saber si se tiene que mover a la derecha
                {
                    if(aux.Derecha.valor!= null) // Verifica que no se encuentre en una hoja
                    {
                        ContadorComparaciones += 1;
                        aux = aux.Derecha; // Se mueve a la derecha
                    }
                    else // Si se encuentra en una hoja entonces no existe el valor buscado
                    {
                        return noexiste.valor;
                    }
                }
            }
            return aux.valor; // El arbol esta vacio
        }

        public void Eliminacion(Nodo<T> padre, T valor, Delegate delegado)
        {
            //Busca el elemento a eliminar con recursividad
            if(Convert.ToInt32(delegado.DynamicInvoke(valor, padre.valor)) == -1) //(valor < padre.valor)
            {
                Eliminacion(padre.Izquierda, valor, delegado);
            }
            else if( Convert.ToInt32(delegado.DynamicInvoke(valor, padre.valor)) == 1) //(valor > padre.valor)
            {
                Eliminacion(padre.Derecha, valor, delegado);
            }
            else if(Convert.ToInt32(delegado.DynamicInvoke(valor, padre.valor)) == 0) //(valor == padre.valor)//Encontro el elemento a eliminar
            {
                NodoBorrar = padre;
                Nodo<T> aux = new Nodo<T>();

                //Comprueba si tiene hijos
                if(padre.Izquierda.valor == null && padre.Derecha.valor == null) //Si cumple entonces no tiene hijos, si no avanza y comprueba de que lado esta el hijo
                {
                    NodoBorrar = new Nodo<T>();
                    padre.valor = NodoBorrar.valor;
                }

                //Verifica si es el hijo izquierdo, si no entonces es el hijo derecho
                else if(padre.Derecha.valor == null)
                {
                    padre = padre.Izquierda; // Se mueve al sub-arbol izquierdo del nodo a eliminar

                    //Se busca la hoja con el valor a remplazar el valor eliminado
                    while(padre.Derecha.valor!= null)
                    {
                        aux = padre; //Se guarda al padre del hijo a remplazar
                        padre = padre.Derecha; // Se guarda el valor mas grande de los menores
                    }
                    NodoBorrar.valor = padre.valor; // Se remplaza el nodo eliminado con el nuevo valor para mantener coherencia en el arbol
                    NodoBorrar = padre.Izquierda; // El NodoBorrar se vuelve null
                    aux.Derecha = NodoBorrar; // Se borra la hoja ya que esta fue remplazada
                }
                //Entrara aqui por dos razones
                // 1. Solo tiene un hijo y es el derecho
                // 2. Tiene los dos hijos y se buscara el elemento mas a la izquierda del sub arbol derecho para remplazar al elemnto eliminado
                else 
                {
                    padre = padre.Derecha; // Se mueve al sub-arbol derecho del nodo a eliminar

                    // Se busca la hoja con el valor con el que se va a remplazar el valor eliminado
                    while(padre.Izquierda.valor!= null)
                    {
                        aux = padre; // Se guarda al padre del hijo que va a remplazar
                        padre = padre.Izquierda; // Se guarda e; valor mas pequeno del lado derecho
                    }
                    NodoBorrar.valor = padre.valor; // Se remplaza  el nodo eliminado con el nuevo valor para mantener coherencia en el arbol
                    NodoBorrar = padre.Derecha; // El NodoBorrar se vuelve null
                    aux.Izquierda = NodoBorrar; // Se borra la hoja ya que esta fue remplazada
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
