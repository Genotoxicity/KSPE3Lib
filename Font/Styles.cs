using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPE3Lib
{
    [Flags]
    public enum Styles
    {
        Bold = 0x01,
        Italic = 0x02,
        Underline = 0x04,
        Strikeout = 0x08,
        Opaque = 0X10
    }
}
