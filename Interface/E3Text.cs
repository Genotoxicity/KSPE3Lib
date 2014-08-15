using e3;
using System.Windows;

namespace KSPE3Lib
{
    public class E3Text
    {
        private e3Text text;
        private e3Graph graph;

        public int Id
        {
            get
            {
                return text.GetId();
            }
            set
            {
                text.SetId(value);
            }
        }

        internal E3Text(int id, E3ObjectFabric e3ObjectFabric)
        {
            text = e3ObjectFabric.GetText(id);
            graph = e3ObjectFabric.GetGraph(0);
        }

        public int CreateText(int sheetId, string value, double x, double y)
        {
            Id = graph.CreateText(sheetId, value, x, y);
            return Id;
        }

        public int CreateText(int sheetId, string value, double x, double y, E3Font font)
        {
            Id = graph.CreateText(sheetId, value, x, y);
            SetFont(font);
            return Id;
        }

        public int CreateVerticalText(int sheetId, string value, double x, double y)
        {
            Id = graph.CreateRotatedText(sheetId, value, x, y, 90);
            return Id;
        }

        public int CreateVerticalText(int sheetId, string value, double x, double y, E3Font font)
        {
            Id = graph.CreateRotatedText(sheetId, value, x, y, 90);
            SetFont(font);
            return Id;
        }

        public void SetFont (E3Font font)
        {
            if (font != null)
            {
                text.SetHeight(font.height);
                text.SetFontName(font.name);
                text.SetAlignment((int)font.alignment);
                text.SetStyle((int)font.style);
            }
        }

        public double GetTextLength(string value, E3Font font)
        {
            dynamic xArray = default(dynamic);
            dynamic yArray = default(dynamic);
            text.CalculateBoxAt(0, value, 0, 0, 0, font.height, (int)font.mode, (int)font.style, font.name, 0, 0, ref xArray, ref yArray); // в качестве начальных координат для простоты устанавливаем 0, 0
            return (double)xArray[2];    // координата X второго угла textBox
        }

        public Size GetTextBoxSize(string value, E3Font font, double rotation)
        {
            dynamic xArray = default(dynamic);
            dynamic yArray = default(dynamic);
            text.CalculateBoxAt(0, value, 0, 0, rotation, font.height, (int)font.mode, (int)font.style, font.name, 0, 0, ref xArray, ref yArray); // в качестве начальных координат для простоты устанавливаем 0, 0
            return new Size(xArray[2], yArray[4]);
        }
    }
}
