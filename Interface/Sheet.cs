using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public class Sheet
    {
        //private E3Objects e3Objects;
        private e3Sheet sheet;
        private OrdinateDirection ordinateDirection;
        private AbscissaDirection abscissaDirection;

        public int Id { get; private set; }

        public string Name
        {
            get
            {
                return sheet.GetName();
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    sheet.SetName(value.Replace(' ', '_'));
            }
        }

        internal Sheet(int id, E3Objects e3Objects)
        {
            //this.e3Objects = e3Objects;
            Id = id;
            sheet = e3Objects.GetSheet(id);
            SetAxesDirections();
        }

        private void SetAxesDirections()
        {
            dynamic xLeft = default(dynamic);
            dynamic yBottom = default(dynamic);
            dynamic xRight = default(dynamic);
            dynamic yTop = default(dynamic);
            sheet.GetDrawingArea(ref xLeft, ref yBottom, ref xRight, ref yTop);
            if (yTop < yBottom)
                ordinateDirection = OrdinateDirection.TopToBottom;
            else
                ordinateDirection = OrdinateDirection.BottomToTop;
            if (xLeft < xRight)
                abscissaDirection = AbscissaDirection.LeftToRight;
            else
                abscissaDirection = AbscissaDirection.RightToLeft;
        }

        public void SetAttribute(string attribute, string value)
        {
            sheet.SetAttributeValue(attribute, value);
        }

        public double MoveDown(double from, double offset)
        {
            if (ordinateDirection == OrdinateDirection.TopToBottom)
                return from + offset;
            return from - offset;
        }

        public double MoveUp(double from, double offset)
        {
            if (ordinateDirection == OrdinateDirection.BottomToTop)
                return from + offset;
            return from - offset;
        }

        public double MoveLeft(double from, double offset)
        {
            if (abscissaDirection == AbscissaDirection.RightToLeft)
                return from + offset;
            return from - offset;
        }

        public double MoveRight(double from, double offset)
        {
            if (abscissaDirection == AbscissaDirection.LeftToRight)
                return from + offset;
            return from - offset;
        }

        public bool IsUnderTarget(double target, double value)
        {
            if (ordinateDirection == OrdinateDirection.TopToBottom)
                return target <= value;
            return target >= value;
        }
    }
}
