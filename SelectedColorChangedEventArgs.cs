using System;
using System.Windows.Media;

namespace KSPE3Lib
{
    public class SelectedColorChangedEventArgs : EventArgs
    {
        public int colorIndex;
        public Color color;

        public SelectedColorChangedEventArgs(int colorIndex, Color color)
            : base()
        {
            this.colorIndex = colorIndex;
            this.color = color;
        }
    }
}
