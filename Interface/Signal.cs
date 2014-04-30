using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e3;

namespace KSPE3Lib
{
    public class Signal
    {
        private e3Signal signal;
        private List<int> devicePinIds;
        private List<int> coreIds;

        public int Id
        {
            get
            {
                return signal.GetId();
            }
            set
            {
                signal.SetId(value);
                devicePinIds = null;
                coreIds = null;
            }
        }

        public List<int> DevicePinIds
        {
            get
            {
                if (devicePinIds == null)
                    devicePinIds = GetDevicePinIds();
                return devicePinIds;
            }
        }

        public List<int> CoreIds
        {
            get
            {
                if (coreIds == null)
                    coreIds = GetCoreIds();
                return coreIds;
            }
        }

        public string Name
        {
            get
            {
                return signal.GetName();
            }
            set
            {
                signal.SetName(value);
            }
        }

        public Signal(int id, E3ObjectFabric e3ObjectFabric)
        {
            signal = e3ObjectFabric.GetSignal(id);
        }

        private List<int> GetDevicePinIds()
        {
            dynamic pinIds = default(dynamic);
            int pinCount = signal.GetPinIds(ref pinIds);
            List<int> ids = new List<int>(pinCount);
            for (int i = 1; i <= pinCount; i++)
                ids.Add(pinIds[i]);
            if (CoreIds.Count > 0)
            {
                IEnumerable<int> except = ids.Except<int>(CoreIds);
                ids = except.ToList<int>();
            }
            return ids;
        }

        private List<int> GetCoreIds()
        {
            dynamic signalCoreIds = default(dynamic);
            int coreCount = signal.GetCoreIds(ref signalCoreIds);
            List<int> ids = new List<int>(coreCount);
            for (int i = 1; i <= coreCount; i++)
                ids.Add(signalCoreIds[i]);
            return ids;
        }

    }
}
