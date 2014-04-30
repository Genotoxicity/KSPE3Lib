using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e3;

namespace KSPE3Lib
{
    public class Connection
    {
        private e3Connection connection;
        private E3ObjectFabric e3ObjectFabric;
        private List<DevicePin> pins;
        private List<int> pinIds;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<DevicePin> Pins
        {
            get
            {
                if (pins == null)
                    pins = GetPins();
                return pins;
            }
        }

        public List<int> PinIds
        {
            get
            {
                if (pinIds == null)
                    pinIds = GetPinIds();
                return pinIds;
            }
        }

        public int Id
        {
            get
            {
                return connection.GetId();
            }
            set
            {
                connection.SetId(value);
                pins = null;
                pinIds = null;
            }
        }

        public string Name
        {
            get
            {
                return connection.GetName();
            }
        }

        internal Connection(int id, E3ObjectFabric e3ObjectFabric)
        {
            this.e3ObjectFabric = e3ObjectFabric;
            connection = e3ObjectFabric.GetConnection(id);
        }

        private List<DevicePin> GetPins()
        {
            List<DevicePin> connectionPins = new List<DevicePin>(PinIds.Count);
            foreach (int pinId in PinIds)
                connectionPins.Add(new DevicePin(pinId, e3ObjectFabric));
            return connectionPins;
        }

        private List<int> GetPinIds()
        {
            dynamic connectionPinIds = default(dynamic);
            int pinCount = connection.GetPinIds(ref connectionPinIds);
            List<int> ids = new List<int>(pinCount);
            for (int i = 1; i <= pinCount; i++)
                ids.Add(connectionPinIds[i]);
            return ids;
        }

        public bool IsUnique()
        {
            return PinIds.Count <= 2;
        }

        public void Highlight()
        {
            connection.Highlight();
        }

    }
}
