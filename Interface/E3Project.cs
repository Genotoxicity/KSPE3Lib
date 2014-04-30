using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;
using System.Windows;

namespace KSPE3Lib
{
    public class E3Project
    {
        private E3ObjectFabric e3ObjectFabric;
        private int undefinedId = -1;
        private int wireGroupId = 0;

        private int WireGroupId
        {
            get
            {
                if (wireGroupId == 0)
                    wireGroupId = GetWireGroupId();
                return wireGroupId;
            }
        }

        public E3Project(int applicationProcessId)
        {
            e3ObjectFabric = new E3ObjectFabric(applicationProcessId);
        }

        public List<WireCore> Wires
        {
            get
            {
                List<int> wireIds = WireIds;
                List<WireCore> wires = new List<WireCore>(wireIds.Count);
                foreach (int wireId in wireIds)
                    wires.Add(new WireCore(wireId, e3ObjectFabric));
                return wires;
            }
        }

        public List<int> WireIds
        {
            get
            {
                if (WireGroupId == undefinedId)   // если проводов не найдено
                    return new List<int>(0);   // возвращаем пустой список
                e3Device wireGroup = e3ObjectFabric.GetDevice(WireGroupId);
                dynamic wireIds = default(dynamic);
                int wireCount = wireGroup.GetCoreIds(ref wireIds);
                List<int> ids = new List<int>(wireCount);
                for (int i = 1; i <= wireCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(wireIds[i]);
                return ids;
            }
        }

        private int GetWireGroupId()
        {
            dynamic cableIds = default(dynamic);
            int cableCount = e3ObjectFabric.GetJob().GetCableIds(ref cableIds);
            e3Device device = e3ObjectFabric.GetJob().CreateDeviceObject();
            for (int i = 1; i <= cableCount; i++)
            {
                device.SetId(cableIds[i]);
                if (device.IsWireGroup() == 1)  // определение устройства "Провода" содержащего в себе все провода в проекте
                    return cableIds[i];
            }
            return undefinedId;   // если проводов не найдено
        }

        public List<CableDevice> Cables
        {
            get
            {
                List<int> cableIds = CableIds;
                List<CableDevice> cables = new List<CableDevice>(cableIds.Count);
                foreach (int cableId in cableIds)
                    cables.Add(new CableDevice(cableId, e3ObjectFabric));
                return cables;
            }
        }

        public List<int> CableIds
        {
            get
            {
                dynamic cableIds = default(dynamic);
                int cableCount = e3ObjectFabric.GetJob().GetCableIds(ref cableIds);
                List<int> ids = new List<int>(cableCount);
                for (int i = 1; i <= cableCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(cableIds[i]);
                ids.Remove(WireGroupId);    // удаляем провода
                return ids;
            }
        }

        public CableDevice GetCableById(int id)
        {
            return new CableDevice(id, e3ObjectFabric);
        }

        public CableCore GetCableCoreById(int id)
        {
            return new CableCore(id, e3ObjectFabric);
        }

        public List<Signal> Signals
        {
            get
            {
                List<int> signalIds = SignalIds;
                List<Signal> signals = new List<Signal>(signalIds.Count);
                foreach (int signalId in signalIds)
                    signals.Add(new Signal(signalId, e3ObjectFabric));
                return signals;
            }
        }

        public List<int> SignalIds
        {
            get
            {
                dynamic signalIds = default(dynamic);
                int signalCount = e3ObjectFabric.GetJob().GetSignalIds(ref signalIds);
                List<int> ids = new List<int>(signalCount);
                for (int i = 1; i <= signalCount; i++)    // e3 в [0] всегда возвращает null
                    ids.Add(signalIds[i]);
                return ids;
            }
        }

        public List<Connection> Connections
        {
            get
            {
                List<int> connectionIds = ConnectionIds;
                List<Connection> connections = new List<Connection>(connectionIds.Count);
                foreach (int connectionId in connectionIds)
                    connections.Add(new Connection(connectionId, e3ObjectFabric));
                return connections;
            }
        }

        public List<int> ConnectionIds
        {
            get
            {
                dynamic connectionIds = default(dynamic);
                int connectionCount = e3ObjectFabric.GetJob().GetAllConnectionIds(ref connectionIds);
                List<int> ids = new List<int>(connectionCount);
                for (int i = 1; i <= connectionCount; i++)  // e3 в [0] всегда возвращает null
                    ids.Add(connectionIds[i]);
                return ids;
            }
        }

        public NormalDevice GetDeviceById(int id)
        {
            return new NormalDevice(id, e3ObjectFabric);
        }

        public DevicePin GetDevicePinById(int id)
        {
            return new DevicePin(id, e3ObjectFabric);
        }

        public Core GetCoreById(int id)
        {
            return new Core(id, e3ObjectFabric);
        }

        public WireCore GetWireCoreById(int id)
        {
            return new WireCore(id, e3ObjectFabric);
        }

        public E3Text GetTextById(int id)
        {
            return new E3Text(id, e3ObjectFabric);
        }

        public Graphic GetGraphicById(int id)
        {
            return new Graphic(id, e3ObjectFabric);
        }

        public Group GetGroupById(int id)
        {
            return new Group(id, e3ObjectFabric);
        }

        public List<int> SheetIds
        {
            get
            {
                dynamic sheetIds = default(dynamic);
                int sheetCount = e3ObjectFabric.GetJob().GetSheetIds(ref sheetIds);
                List<int> ids = new List<int>(sheetCount);
                for (int i = 1; i <= sheetCount; i++)
                    ids.Add(sheetIds[i]);
                return ids;
            }
        }

        public Sheet GetSheetById(int id)
        {
            return new Sheet(id, e3ObjectFabric);
        }

        public int CreateSheet(string name, string format)
        {
            return CreateSheet(name, format, 0, Position.After);
        }

        public int CreateSheet(string name, string format, int targetSheetId, Position position)
        {
            int newSheetId = e3ObjectFabric.GetSheet(0).Create(0, name, format, targetSheetId, (int)position);
            return newSheetId;
        }

        public Signal GetSignalById(int id)
        {
            return new Signal(id, e3ObjectFabric);
        }

        public Symbol GetSymbolById(int id)
        {
            return new Symbol(id, e3ObjectFabric);
        }

        public Connection GetConnectionById(int id)
        {
            return new Connection(id, e3ObjectFabric);
        }

        public int JumpToId(int id)
        {
            return e3ObjectFabric.GetJob().JumpToID(id);
        }

        public void Release()
        {
            e3ObjectFabric.Release();
        }

    }
}
