using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public class WireCore : Core
    {
        public string WireType
        {
            get
            {
                dynamic wireType = default(dynamic);
                dynamic wireName = default(dynamic);
                pin.GetWireType(ref wireType, ref wireName);
                return wireType;
            }
        }

        internal WireCore(int id, E3ObjectFabric e3ObjectFabric)
            : base(id, e3ObjectFabric)
        {
        }
    }
}
