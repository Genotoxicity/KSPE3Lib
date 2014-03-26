using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3;

namespace KSPE3Lib
{
    public class Text
    {
        //private E3Objects e3Objects;
        private e3Text text;

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

        internal Text(int id, E3Objects e3Objects)
        {
            //this.e3Objects = e3Objects;
            text = e3Objects.GetText(id);
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
    }
}
