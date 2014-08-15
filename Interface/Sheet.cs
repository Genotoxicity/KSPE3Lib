using System;
using System.Collections.Generic;
using e3;

namespace KSPE3Lib
{
    public class Sheet
    {
        private e3Sheet sheet;
        private E3ObjectFabric objectFabric;
        private OrdinateDirection ordinateDirection;
        private AbscissaDirection abscissaDirection;
        private Area drawingArea;
        private bool isDrawingAreaGot;

        public int Id
        {
            get
            {
                return sheet.GetId();
            }
            set
            {
                sheet.SetId(value);
                SetAxesDirections();
                isDrawingAreaGot = false;
            }
        }   

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

        public List<int> SymbolIds
        {
            get
            {
                dynamic symbolIds = default(dynamic);
                int symbolCount = sheet.GetSymbolIds(ref symbolIds);
                List<int> ids = new List<int>(symbolCount);
                for (int i = 1; i <= symbolCount; i++)
                    ids.Add(symbolIds[i]);
                return ids;
            }
        }

        public Area DrawingArea
        {
            get
            {
                if (!isDrawingAreaGot)
                {
                    drawingArea = GetDrawingArea();
                    isDrawingAreaGot = true;
                }
                return drawingArea;
            }
        }

        internal Sheet(int id, E3ObjectFabric e3ObjectFabric)
        {
            sheet = e3ObjectFabric.GetSheet(id);
            objectFabric = e3ObjectFabric;
            SetAxesDirections();
            isDrawingAreaGot = false;
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

        public bool IsSchematicTypeOf(int schematicTypeCode)
        {
            dynamic schematicTypes = default(dynamic);
            int schematicTypeCount = sheet.GetSchematicTypes(ref schematicTypes);
            if (schematicTypeCount == 0)
                return false;
            for (int i = 1; i <= schematicTypeCount; i++)
                if (schematicTypes[i] == schematicTypeCode)
                    return true;
            return false;
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

        public bool IsRightOfTarget(double target, double value)
        {
            if (abscissaDirection == AbscissaDirection.LeftToRight)
                return target <= value;
            return target >= value;
        }

        public int Create(string name, string format)
        {
            return Create(name, format, 0, Position.After);
        }

        public int Create(string name, string format, int targetSheetId, Position position)
        {
            int newSheetId = objectFabric.GetSheet(0).Create(0, name, format, targetSheetId, (int)position);
            Id = newSheetId;
            return newSheetId;
        }

        public int Delete()
        {
            if (Id > 0)
            {
                int result = sheet.Delete();
                Id = 0;
                return result;
            }
            return 0;
        }

        private Area GetDrawingArea()
        { 
            dynamic xMin = default(dynamic), yMin = default(dynamic), xMax = default(dynamic), yMax = default(dynamic);
            sheet.GetDrawingArea(ref xMin, ref yMin, ref xMax, ref yMax);
            return new Area(xMin, xMax, yMax, yMin);
        }
    }
}
