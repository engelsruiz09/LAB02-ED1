using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es_Arboles
{
    //abstract class puede ser heredada por otras clases y que es accesible desde cualquier parte del codigo
    public abstract class NoLinear<T> where T : IComparable<T>
    {
        protected abstract Node<T> Insert(Node<T> nodo, T value);
        protected abstract void Delete(Node<T> nodo);
        protected abstract Node<T> Get(Node<T> nodo, T value);
    }
}
