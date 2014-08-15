using System.Collections.Generic;

namespace KSPE3Lib
{
    public class NormalDevice : Device
    {
        internal NormalDevice(int id, E3ObjectFabric e3ObjectFabric)
            : base(id, e3ObjectFabric)
        { 
        
        }

        public List<int> GetSymbolIds(SymbolReturnParameter parameter)
        {
            dynamic symbolIds = default(dynamic);
            int symbolCount = device.GetSymbolIds(ref symbolIds, (int) parameter);
            List<int> ids = new List<int>(symbolCount);
            for (int i = 1; i <= symbolCount; i++)
                ids.Add(symbolIds[i]);
            return ids;
        }
    }
}
