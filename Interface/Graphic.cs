using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public class Graphic
    {
        private e3Graph graph;

        public int Id
        {
            get
            {
                return graph.GetId();
            }
            set
            {
                graph.SetId(value);
            }
        }

        internal Graphic(int id, E3ObjectFabric e3ObjectFabric)
        {
            graph = e3ObjectFabric.GetGraph(id);
        }

        public int CreateText(int sheetId, string value, double x, double y)
        {
            return graph.CreateText(sheetId, value, x, y);
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2)
        {
            return graph.CreateLine(sheetId, x1, y1, x2, y2);
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2, double width)
        {
            int graphicId = CreateLine(sheetId, x1, y1, x2, y2);
            SetLineWidth(width);
            return graphicId;
        }

        private void SetLineWidth(double width)
        {
            graph.SetLineWidth(width);
        }

    }
}
