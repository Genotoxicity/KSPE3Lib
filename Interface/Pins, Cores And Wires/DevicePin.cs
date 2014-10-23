using System.Collections.Generic;
using System.Windows;
using System;
using e3;

namespace KSPE3Lib
{
    public class DevicePin : Pin
    {
        private int sheetId;
        private Point position;
        private bool isLocationVariablesSet;

        public List<int> CoreIds
        {
            get
            {
                dynamic connectedCoreIds = default(dynamic);
                int coreCount = pin.GetCoreIds(ref connectedCoreIds);
                List<int> ids = new List<int>(coreCount);
                for (int i = 1; i <= coreCount; i++)
                    ids.Add(connectedCoreIds[i]);
                return ids;
            }
        }

        public int SequenceNumber
        {
            get
            {
                return pin.GetSequenceNumber();
            }
        }

        public int PhysicalId
        {
            get
            {
                return pin.GetPhysicalID();
            }
        }

        public bool IsPlaced
        {
            get
            {
                return pin.HasDevice() == 1;
            }
        }

        public override int Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
                isLocationVariablesSet = false;
            }
        }

        public int SheetId
        {
            get
            {
                if (!isLocationVariablesSet)
                    SetLocationVariables();
                return sheetId;
            }
        }

        public Point Position
        {
            get
            {
                if (!isLocationVariablesSet)
                    SetLocationVariables();
                return position;
            }
        }

        public int LogicalEquivalence
        {
            get
            {
                return pin.GetLogicalEquivalenceID();
            }
        }

        public int NameEquivalence
        {
            get
            {
                return pin.GetNameEquivalenceID();
            }
        }

        public bool IsOnPanel
        {
            get
            {
                dynamic dx = default(dynamic);
                dynamic dy = default(dynamic);
                dynamic dz = default(dynamic);
                return pin.GetPanelLocation(ref dx, ref dy, ref dz) > 0;
            }
        }

        internal DevicePin(int id, E3ObjectFabric e3ObjectFabric) : base(id, e3ObjectFabric)
        {
            isLocationVariablesSet = false;
        }

        public bool IsView()
        {
            return pin.IsView() == 1;
        }

        public bool IsPinView()
        {
            return pin.IsPinView() == 1;
        }

        public int GetPanelLocation(out double x, out double y, out double z)
        {
            dynamic dx = default(dynamic);
            dynamic dy = default(dynamic);
            dynamic dz = default(dynamic);
            int result = pin.GetPanelLocation(ref dx, ref dy, ref dz);
            x = (double)dx;
            y = (double)dy;
            z = (double)dz;
            return result;
        }

        private void SetLocationVariables()
        {
            isLocationVariablesSet = true;
            dynamic xCoordinate = default(dynamic), yCoordinate = default(dynamic), grid = default(dynamic);
            sheetId = pin.GetSchemaLocation(ref xCoordinate, ref yCoordinate, ref grid);
            if (xCoordinate != null && yCoordinate != null)
                position = new Point(xCoordinate, yCoordinate);
        }

    }
}
