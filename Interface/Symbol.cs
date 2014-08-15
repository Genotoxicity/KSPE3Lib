using System.Collections.Generic;
using System;
using e3;

namespace KSPE3Lib
{
    public class Symbol
    {
        private e3Symbol symbol;
        private List<int> pinIds;
        private bool isAreaGot;
        private Area area;

        public Area Area
        {
            get
            {
                if (!isAreaGot)
                {
                    area = GetArea();
                    isAreaGot = true;
                }
                return area;
            }
        }

        public int Id
        {
            get
            {
                return symbol.GetId();
            }
            set
            {
                symbol.SetId(value);
                pinIds = null;
                isAreaGot = false;
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

        public List<int> PinIds
        {
            get
            {
                if (pinIds == null)
                    pinIds = GetPinIds();
                return pinIds;
            }
        }

        public int PinCount
        {
            get
            {
                return symbol.GetPinCount();
            }
        }

        internal Symbol(int id, E3ObjectFabric e3ObjectFabric)
        {
            symbol = e3ObjectFabric.GetSymbol(id);
            isAreaGot = false;
        }

        private List<int> GetPinIds()
        {
            dynamic pinIds = default(dynamic);
            int pinCount = symbol.GetPinIds(ref pinIds);
            List<int> ids = new List<int>(pinCount);
            for (int i = 1; i <= pinCount; i++)
                ids.Add(pinIds[i]);
            return ids;
        }

        public bool IsSchematicTypeOf(int schematicTypeCode)
        {
            dynamic schematicTypes = default(dynamic);
            int schematicTypeCount = symbol.GetSchematicTypes(ref schematicTypes);
            if (schematicTypeCount == 0)
                return false;
            for (int i = 1; i <= schematicTypeCount; i++)
                if (schematicTypes[i] == schematicTypeCode)
                    return true;
            return false;
        }

        public int Place(int sheetId, double x, double y)
        {
            return symbol.Place(sheetId, x, y);
        }

        public int Place(int sheetId, double x, double y, SymbolTransformation transformation)
        {
            string rotation = String.Empty;
            if (transformation == SymbolTransformation.HorizontallyMirrored)
                rotation = "X0";
            return symbol.Place(sheetId, x, y,rotation);
        }

        public int Delete()
        {
            return symbol.Delete();
        }

        private Area GetArea()
        {
            dynamic xMin = default(dynamic), yMin = default(dynamic), xMax = default(dynamic), yMax = default(dynamic);
            symbol.GetArea(ref xMin, ref yMin, ref xMax, ref yMax);
            return new Area(xMin, xMax, yMax, yMin);
        }

    }
}
