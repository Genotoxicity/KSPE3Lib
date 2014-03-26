using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;
using System.Windows;

namespace KSPE3Lib
{
    public class Project
    {
        private ProjectDispatcher dispatcher;
        private Status status;

        private int appProcessId;
        private E3Objects e3Objects;

        private int undefinedId = -1;

        private e3Application App
        {
            get
            {
                return dispatcher.GetE3ApplicationByProcessId(appProcessId);
            }
        }

        public Status Status
        {
            get
            {
                if (App != null)
                {
                    try
                    {
                        App.GetName();  // проверка на различные COM ошибки
                    }
                    catch
                    {
                        status = Status.Error;
                    }
                }
                return status;
            }
        }

        public String Name { get; private set; }

        public Project()
        {
            dispatcher = new ProjectDispatcher();
            appProcessId = dispatcher.GetApplicationProcessId(ref status);
            if (Status == Status.Selected)
                Name = App.CreateJobObject().GetName();
        }

        public void Seize()
        {
            e3Objects = new E3Objects(App.CreateJobObject());
        }

        public List<WireCore> GetWires()
        {
            int wireGroupId = GetWireGroupId();
            if (wireGroupId == undefinedId )   // если проводов не найдено
                return new List<WireCore>(0);   // возвращаем пустой список
            e3Device wireGroup = e3Objects.GetDevice(wireGroupId);
            dynamic wireIds = default(dynamic);
            int wireCount = wireGroup.GetCoreIds(ref wireIds);
            List<WireCore> wires = new List<WireCore>(GetValidObjectCount(wireCount));
            for (int i = 1; i <= wireCount; i++)    // e3 в [0] всегда возвращает null
                wires.Add(new WireCore(wireIds[i], e3Objects));
            return wires;
        }

        private int GetWireGroupId()
        {
            e3Job project = App.CreateJobObject();
            dynamic cableIds = default(dynamic);
            int cableCount = project.GetCableIds(ref cableIds);
            e3Device device = project.CreateDeviceObject();
            for (int i = 1; i <= cableCount; i++)
            {
                device.SetId(cableIds[i]);
                if (device.IsWireGroup() == 1)  // определение устройства "Провода" содержащего в себе все провода в проекте
                    return cableIds[i];
            }
            return undefinedId;   // если проводов не найдено
        }

        public List<CableDevice> GetCables()
        {
            e3Job project = App.CreateJobObject();
            dynamic cableIds = default(dynamic);
            int cableCount = project.GetCableIds(ref cableIds);
            List<CableDevice> cables = new List<CableDevice>(GetValidObjectCount(cableCount));
            e3Device device = project.CreateDeviceObject();
            for (int i = 1; i <= cableCount; i++)    // e3 в [0] всегда возвращает null
            {
                device.SetId(cableIds[i]);
                if (device.IsWireGroup() == 0)   // учитываем только кабеля, без проводов
                    cables.Add(new CableDevice(cableIds[i], e3Objects));
            }
            return cables;
        }

        public NormalDevice GetDeviceById(int id)
        {
            return new NormalDevice(id, e3Objects);
        }

        public DevicePin GetPinById(int id)
        {
            return new DevicePin(id, e3Objects);
        }

        public Text GetTextById(int id)
        {
            return new Text(id, e3Objects);
        }

        public Graphic GetGraphicById(int id)
        {
            return new Graphic(id, e3Objects);
        }

        public Group GetGroupById(int id)
        {
            return new Group(id, e3Objects);
        }

        public Sheet GetSheetById(int id)
        {
            return new Sheet(id, e3Objects);
        }

        public Sheet CreateSheet(string name, string format)
        {
            return CreateSheet(name, format, 0, Position.After);
        }

        public Sheet CreateSheet(string name, string format, int targetSheetId, Position position)
        {
            int newSheetId = e3Objects.GetSheet(0).Create(0, name, format, targetSheetId, (int)position);
            return new Sheet(newSheetId, e3Objects);
        }

        private static int GetValidObjectCount(int totalCount)
        {
            return Math.Max(0, totalCount - 1);
        }

        public void Release()
        {
            e3Objects.Release();
            GC.Collect();
        }

    }
}
