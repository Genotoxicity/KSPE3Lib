using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public class Device
    {
        protected E3Objects e3Objects;
        protected e3Device device;
        protected int id;

        public int Id
        {
            get
            {
                return id;
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

        public string Component
        {
            get
            {
                return e3Objects.GetComponent(id).GetName();
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

        protected Device(int id, E3Objects e3Objects)
        {
            this.e3Objects = e3Objects;
            this.id = id;
            device = e3Objects.GetDevice(id);
        }

    }
}
