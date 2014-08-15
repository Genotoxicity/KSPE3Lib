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
            graph = e3ObjectFabric.GetGraph(id);;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2)
        {
            Id = graph.CreateLine(sheetId, x1, y1, x2, y2);
            return Id;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2, int colorIndex)
        {
            Id = graph.CreateLine(sheetId, x1, y1, x2, y2);
            graph.SetColour(colorIndex);
            return Id;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2, double width)
        {
            Id = graph.CreateLine(sheetId, x1, y1, x2, y2);
            graph.SetLineWidth(width);
            return Id;
        }

        public int CreateLine(int sheetId, double x1, double y1, double x2, double y2, double width, int colorIndex)
        {
            Id = graph.CreateLine(sheetId, x1, y1, x2, y2);
            graph.SetLineWidth(width);
            graph.SetColour(colorIndex);
            return Id;
        }

        public int CreateRectangle(int sheetId, double x1, double y1, double x2, double y2)
        {
            Id = graph.CreateRectangle(sheetId, x1, y1, x2, y2);
            return Id;
        }

        public int CreateRectangle(int sheetId, double x1, double y1, double x2, double y2, double width)
        {
            Id = graph.CreateRectangle(sheetId, x1, y1, x2, y2);
            graph.SetLineWidth(width);
            return Id;
        }

        public int CreateCircle(int sheetId, double x, double y, double radius )
        {
            Id = graph.CreateCircle(sheetId, x, y, radius);
            return Id;
        }

        public int CreateArc(int sheetId, double x, double y, double radius, double startAngle, double endAngle)
        {
            Id = graph.CreateArc(sheetId, x, y, radius, startAngle, endAngle);
            return Id;
        }

        public int CreateArc(int sheetId, double x, double y, double radius, double startAngle, double endAngle, double width, int colorIndex)
        {
            Id = graph.CreateArc(sheetId, x, y, radius, startAngle, endAngle);
            graph.SetLineWidth(width);
            graph.SetColour(colorIndex);
            return Id;
        }

        public void SetLineWidth(double width)
        {
            graph.SetLineWidth(width);
        }

        public void SetColor(int colorIndex)
        {
            graph.SetColour(colorIndex);
        }

    }
}
