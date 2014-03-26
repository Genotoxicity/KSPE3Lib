using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPE3Lib
{
    public class LineTemplate
    {
        public List<double> ColumnWidths { get; private set; }

        public double Height { get; private set; }

        public double Width { get; private set; }

        public double VerticalLineWidth { get; private set; }

        public double HorizontalLineWidth { get; private set; }

        public List<double> TextHorizontalOffsets { get; private set; }

        public double TextVerticalOffset { get; private set; }

        public E3Font Font { get; private set; }

        public LineTemplate(List<double> columnWidths, E3Font font, double lineHeight, double verticalLineWidth, double horizontalLineWidth)
        {
            ColumnWidths = columnWidths;
            Height = lineHeight;
            Width = columnWidths.Sum();
            Font = font;
            VerticalLineWidth = verticalLineWidth;
            HorizontalLineWidth = horizontalLineWidth;
            TextHorizontalOffsets = GetTextHorizontalOffsets(columnWidths);
            TextVerticalOffset = (lineHeight + font.height) / 2;
        }

        private static List<double> GetTextHorizontalOffsets(IEnumerable<double> columnWidths)
        {
            List<double> textHorizontalOffsets = new List<double>(columnWidths.Count<double>());
            double totalOffset = 0;
            foreach (double columnWidth in columnWidths)
            {
                double offset = totalOffset + (columnWidth / 2);
                textHorizontalOffsets.Add(offset);
                totalOffset += columnWidth;
            }
            return textHorizontalOffsets;
        }
    }
}
