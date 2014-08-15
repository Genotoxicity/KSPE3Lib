using e3;

namespace KSPE3Lib
{
    public class Device
    {
        protected E3ObjectFabric e3ObjectFabric;
        protected e3Device device;
        protected e3Component component;

        public virtual int Id
        {
            get
            {
                return device.GetId();
            }
            set
            {
                device.SetId(value);
                if (component != null)
                    component.SetId(value);
            }
        }

        public string Name
        {
            get
            {
                return device.GetName();
            }
            set
            {
                device.SetName(value);
            }
        }

        public string ComponentName
        {
            get
            {
                if (component == null)
                    component = e3ObjectFabric.GetComponent(Id);
                return component.GetName();
            }
        }

        public string Assignment
        {
            get
            {
                return device.GetAssignment();
            }
        }

        public string Location
        {
            get
            {
                return device.GetLocation();
            }
        }

        protected Device(int id, E3ObjectFabric e3ObjectFabric)
        {
            this.e3ObjectFabric = e3ObjectFabric;
            device = e3ObjectFabric.GetDevice(id);
        }

        public bool IsCable()
        {
            if (device.IsCable() == 1)
                return true;
            return false;
        }

        public bool IsWireGroup()
        {
            if (device.IsWireGroup() == 1)
                return true;
            return false;
        }

        public bool IsTerminal()
        {
            if (device.IsTerminal() == 1)
                return true;
            return false;
        }

        public string GetAttributeValue(string attributeName)
        {
            return device.GetAttributeValue(attributeName);
        }

    }
}
