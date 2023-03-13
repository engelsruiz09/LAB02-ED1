namespace LAB02_ED1_G.Models.Datos
{
    public class Singleton
    {
        public int flag;
        public List<ExtensionVehiculo> Aux = new List<ExtensionVehiculo>();
        public Es_Arboles.ABB<ExtensionVehiculo> ArbolVehiculos = new Es_Arboles.ABB<ExtensionVehiculo>();
        public Es_Arboles.AVL<ExtensionVehiculo> AVL = new Es_Arboles.AVL<ExtensionVehiculo>();

        private static Singleton _instance = null;

        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }

    }
}
