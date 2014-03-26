using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public abstract class Pin
    {
        //protected E3Objects e3Objects;
        protected int id;
        protected e3Pin pin;

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
                return pin.GetName();
            }
            set
            {
                pin.SetName(value);
            }
        }

        public string SignalName
        {
            get
            {
                return pin.GetSignalName();
            }
            set
            {
                pin.SetSignalName(value);
            }

        }

        protected Pin(int id, E3Objects e3Objects)
        {
            //this.e3Objects = e3Objects;
            this.id = id;
            pin = e3Objects.GetPin(id);
        }
    }
}
