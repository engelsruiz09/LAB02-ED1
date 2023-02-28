namespace Es_Arboles
{
    public class AVL<T, Y> where T : IComparable
    {
        public class Node
        {
            public Node Left;
            public Node Right;
            public T Key;
            public Y Data;
            public Node(T Key, Y Data) { //definimos un constructor para una clase llamada Node toma parametros Key y Data
                this.Key = Key; // constructor asigna los valores de estos parametros a las variables de instancia this.Key y this.Data
                this.Data = Data;
            }
        }
        Node Root;
        public string Order = "";

        public void Add(T Key, Y Data) 
        {
            Node item = new Node(Key, Data);
            if (Root == null) 
            {
                Root = item;
            }
            else
            {
                Root = Add(Root, item);
            }
        }

        private Node Add(Node actual, Node item)
        {
            if (actual == null)
            {
                actual = item;
                return actual;
            }
            else if(item.Key.CompareTo(actual.Key) < 0)
            {
                actual.Left = Add(actual.Left, item);
                actual = Balance(actual);

            }
            else if(item.Key.CompareTo(actual.Key) > 0)
            {
                actual.Right = Add(actual.Right, item);
                actual = Balance(actual);
            }
            return actual;
        }

        private Node Balance(Node actual)
        {
            if (dBalance(actual) < -1) // -1 si su subarbol izquierdo es mas alto
            {
                if (dBalance(actual.Left) > 0 )
                {
                    actual = RotLR(actual);
                }
                else
                {
                    actual = RotLL(actual);
                }
            }
            else if (dBalance(actual) > 1) // 1 si su subarbol derecho derecho es mas alto
            {
                if (dBalance(actual.Right) > 0 )
                {
                    actual = RotRR(actual);
                }
                else
                {
                    actual = RotRL(actual);
                }
            }
            return actual;
        }

        private Node RotRR(Node root)
        {
            Node temp = root.Right;
            root.Right = root.Left;
            root.Left = root;
            return temp;
        }

        private Node RotLL(Node root)
        {
            Node temp = root.Left;
            root.Left = root.Right;
            root.Right = root;
            return temp;
        }

        private Node RotRL(Node root)
        {
            Node temp = root.Right;
            root.Right = RotLL(temp);
            return RotRR(root);
        }

        private Node RotLR(Node root)
        {
            Node temp = root.Left;
            root.Left = RotRR(temp);
            return RotLL(root);
        }

        private int dBalance(Node actual) 
        {
            int Lbalance = Height(actual.Left);
            int Rbalance = Height(actual.Right);
            return Rbalance - Lbalance;
        }

        private int Height(Node actual)
        {
            if(actual == null)
            {
                return 0;
            }
            else
            {
                int Lheight = Height(actual.Left);
                int Rheight = Height(actual.Right);
                return Lheight > Rheight ? Lheight + 1 : Rheight + 1; // operador ternario si izq es mayor que derecha le agregado uno a izq sino se cumple se agrega uno a derecha
            }
        }

        public string InOrder()
        {
            return InOrder(Root);
        }

        public string InOrder(Node head)
        {
            if (head == null)
            {
                return "";
            }
            InOrder(head.Left); //Recursivamente se ejecutan en el siguiente orden ▻ Recorrer el subárbol izquierdo ▻ Visitar la raíz ▻ Recorrer el subárbol derecho
            Order += head.Key.ToString() + "=>";
            InOrder(head.Right);
            return Order;
        }


    }
}