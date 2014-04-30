using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using e3;

namespace KSPE3Lib
{
    static class E3ColorTable
    {
        public static Dictionary<int, Color> GetColorTable(int processId)
        {
            int maxColorIndex = 255;
            E3ObjectFabric e3Objects = new E3ObjectFabric(processId);
            Dictionary<int, Color> colors = new Dictionary<int, Color>();
            colors.Add(-1, Colors.Black);
            dynamic r = default(dynamic);
            dynamic g = default(dynamic);
            dynamic b = default(dynamic);
            e3Job job = e3Objects.GetJob();
            for (int i = 0; i <= maxColorIndex; i++)
            {
                job.GetRGBValue(i, ref r, ref g, ref b);
                colors.Add(i, Color.FromArgb(0xFF, (byte)r, (byte)g, (byte)b));
            }
            job = null;
            e3Objects.Release();
            return colors;
        }
    }
}
