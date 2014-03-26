using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public class E3Objects
    {
        private e3Job project;

        internal E3Objects(e3Job project)
        {
            this.project = project;
        }

        internal e3Device GetDevice(int id)
        {
            e3Device device = project.CreateDeviceObject();
            device.SetId(id);
            return device;
        }

        internal e3Component GetComponent(int id)
        {
            e3Component component = project.CreateComponentObject();
            component.SetId(id);
            return component;
        }

        internal e3Pin GetPin(int id)
        {
            e3Pin pin = project.CreatePinObject();
            pin.SetId(id);
            return pin;
        }

        internal e3Sheet GetSheet(int id)
        {
            e3Sheet sheet = project.CreateSheetObject();
            sheet.SetId(id);
            return sheet;
        }

        internal e3Symbol GetSymbol(int id)
        {
            e3Symbol symbol = project.CreateSymbolObject();
            symbol.SetId(id);
            return symbol;
        }

        internal e3Graph GetGraph(int id)
        {
            e3Graph graph = project.CreateGraphObject();
            graph.SetId(id);
            return graph;
        }

        internal e3Text GetText(int id)
        {
            e3Text text = project.CreateTextObject();
            text.SetId(id);
            return text;
        }

        internal e3Group GetGroup(int id)
        {
            e3Group group = project.CreateGroupObject();
            group.SetId(id);
            return group;
        }

        internal void Release()
        {
            project = null;
        }
    }
}
