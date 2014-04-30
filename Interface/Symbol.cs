using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e3;

namespace KSPE3Lib
{
    public class Symbol
    {
        private e3Symbol symbol;

        public int Id
        {
            get
            {
                return symbol.GetId();
            }
            set
            {
                symbol.SetId(value);
            }
        }

        public string Name
        {
            get
            {
                return symbol.GetName();
            }
        }

        public string TypeName
        {
            get
            {
                return symbol.GetTypeName();
            }
        }

        internal Symbol(int id, E3ObjectFabric e3ObjectFabric)
        {
            symbol = e3ObjectFabric.GetSymbol(id);
        }

    }
}
