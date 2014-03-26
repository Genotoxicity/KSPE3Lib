using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPE3Lib
{
    public class TablePageTemplate
    {
        public string Format { get; private set; }
        public double UttermostPositionByY { get; private set; }
        public double HeaderLineX { get; private set; }
        public double HeaderLineY { get; private set; }
        public HeaderText HeaderText {get; private set;}

        public TablePageTemplate(string format, double headerLineX, double headerLineY, double uttermostPositionByY) : this (format, headerLineX, headerLineY, uttermostPositionByY, null)
        {

        }

        public TablePageTemplate(string format, double headerLineX, double headerLineY, double uttermostPositionByY, HeaderText headerText)
        {
            Format = format;
            HeaderLineX = headerLineX;
            HeaderLineY = headerLineY;
            UttermostPositionByY = uttermostPositionByY;
            HeaderText = headerText;
        }
    }
}
