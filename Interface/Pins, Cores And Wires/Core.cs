using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public abstract class Core : Pin
    {
        public int FirstEndPinId
        {
            get
            {
                return pin.GetEndPinId(1);
            }
        }

        public int SecondEndPinId
        {
            get
            {
                return pin.GetEndPinId(2);
            }
        }

        protected Core(int id, E3Objects e3Objects)
            : base(id, e3Objects)
        {
        }
    }
}
