﻿using System.Collections.Generic;
using System.Windows;
using System;
using e3;

namespace KSPE3Lib
{
    public class Connection
    {
        private e3Connection connection;
        //private E3ObjectFabric e3ObjectFabric;

        public List<int> PinIds
        {
            get
            {
                dynamic connectionPinIds = default(dynamic);
                int pinCount = connection.GetPinIds(ref connectionPinIds);
                List<int> ids = new List<int>(pinCount);
                for (int i = 1; i <= pinCount; i++)
                    ids.Add(connectionPinIds[i]);
                return ids;
            }
        }

        public int Id
        {
            get
            {
                return connection.GetId();
            }
            set
            {
                connection.SetId(value);
            }
        }

        public string Name
        {
            get
            {
                return connection.GetName();
            }
        }

        internal Connection(int id, E3ObjectFabric e3ObjectFabric)
        {
            //this.e3ObjectFabric = e3ObjectFabric;
            connection = e3ObjectFabric.GetConnection(id);
        }

        public bool IsUnique()
        {
            return PinIds.Count <= 2;
        }

        public void Highlight()
        {
            connection.Highlight();
        }

        public int Create(int sheetId, List<Point> points)
        {
            int pointCount = points.Count;
            dynamic arrayOfX = Array.CreateInstance(typeof(object), pointCount + 1); // e3 работает только с массивами, начинающимися с null
            dynamic arrayOfY = Array.CreateInstance(typeof(object), pointCount + 1); 
            arrayOfX.SetValue(null, 0);
            arrayOfY.SetValue(null, 0);
            for (int i = 0; i < pointCount; i++)
            {
                arrayOfX.SetValue(points[i].X, i + 1);
                arrayOfY.SetValue(points[i].Y, i + 1);
            }
            return connection.Create(sheetId, pointCount, ref arrayOfX, ref arrayOfY);
        }

    }
}
