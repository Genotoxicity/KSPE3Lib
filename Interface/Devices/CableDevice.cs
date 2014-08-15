using System.Collections.Generic;

namespace KSPE3Lib
{
    public class CableDevice : Device
    {
        private List<int> coreIds;
        private List<CableCore> cores;

        public List<int> CoreIds
        {
            get
            {
                if (coreIds == null)
                    coreIds = GetCoreIds();
                return coreIds;
            }
        }

        public List<CableCore> Cores
        {
            get
            {
                if (cores == null)
                    cores = GetCores();
                return cores;
            }
        }

        public override int Id
        {
            get
            {
                
                return base.Id;
            }
            set
            {
                base.Id = value;
                cores = null;
                coreIds = null;
            }
        }

        internal CableDevice(int id, E3ObjectFabric e3ObjectFabric)
            : base(id, e3ObjectFabric)
        {
        }

        private List<CableCore> GetCores()
        {
            List<CableCore> cableCores = new List<CableCore>(CoreIds.Count);
            foreach (int coreId in coreIds)
                cableCores.Add(new CableCore(coreId, e3ObjectFabric));
            return cableCores;
        }

        private List<int> GetCoreIds()
        {
            dynamic cableCoreIds = default(dynamic);
            int coreCount = device.GetCoreIds(ref cableCoreIds);
            List<int> ids = new List<int>(coreCount);
            for (int i = 1; i <= coreCount; i++)
                ids.Add(cableCoreIds[i]);
            return ids;
        }
    }
}
