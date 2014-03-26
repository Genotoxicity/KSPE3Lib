using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public class CableDevice : Device
    {
        private List<CableCore> cores;

        public List<CableCore> Cores
        {
            get
            {
                if (cores == null)
                    cores = GetCores();
                return cores;
            }
        }

        internal CableDevice(int id, E3Objects e3Objects)
            : base(id, e3Objects)
        {
        }

        private List<CableCore> GetCores()
        {
            dynamic coreIds = default(dynamic);
            int coreCount = device.GetCoreIds(ref coreIds);
            int capacity = Math.Max(0, coreCount - 1);
            List<CableCore> cableCores = new List<CableCore>(capacity);
            for(int i=1; i<=coreCount; i++)
                cableCores.Add(new CableCore(coreIds[i], e3Objects));
            return cableCores;
        }

    }
}
