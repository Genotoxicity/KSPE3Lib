﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public class Group
    {
        //private E3Objects e3Objects;
        private e3Group group;

        public int Id { get; private set; }

        internal Group(int id, E3Objects e3Objects)
        {
            Id = id;
            //this.e3Objects = e3Objects;
            group = e3Objects.GetGroup(id);
        }

        public int CreateGroup(List<int> ids)
        {
            if (ids!= null && ids.Count > 0)
            {
                dynamic array = Array.CreateInstance(typeof(object), ids.Count + 1); // чтобы группа создалась, необходимо чтобы id объектов представлялись в виде Object[] с первым значением null
                array.SetValue(null, 0);
                for (int i = 0; i < ids.Count; i++)
                    array.SetValue(ids[i], i + 1);
                return group.Create(ref array);
            }
            return 0;
        }
    }
}