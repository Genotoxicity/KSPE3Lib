namespace KSPE3Lib
{
    public class Core : Pin
    {
        public int StartPinId
        {
            get
            {
                return pin.GetEndPinId(1);
            }
        }

        public int EndPinId
        {
            get
            {
                return pin.GetEndPinId(2);
            }
        }

        internal Core(int id, E3ObjectFabric e3ObjectFabric)
            : base(id, e3ObjectFabric)
        {
        }
    }
}
