using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public class DevicePin : Pin
    {
        private List<int> coreIds;

        public List<int> CoreIds
        {
            get
            {
                if (coreIds == null)
                    coreIds = GetCoreIds();
                return coreIds;
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
                coreIds = null;
            }
        }

        public int SheetId
        {
            get
            {
                dynamic x = default(dynamic), y = default(dynamic), grid = default(dynamic);
                return pin.GetSchemaLocation(ref x, ref y, ref grid);
            }
        }

        internal DevicePin(int id, E3ObjectFabric e3ObjectFabric) : base(id, e3ObjectFabric)
        {
        }

        private List<int> GetCoreIds()
        {
            dynamic connectedCoreIds = default(dynamic);
            int coreCount = pin.GetCoreIds(ref connectedCoreIds);
            List<int> ids = new List<int>(coreCount);
            for (int i = 1; i <= coreCount; i++)
                ids.Add(connectedCoreIds[i]);
            return ids;
        }

        public bool IsView()
        {
            return pin.IsView() == 1;
        }

        public bool IsPinView()
        {
            return pin.IsPinView() == 1;
        }

    }
}
