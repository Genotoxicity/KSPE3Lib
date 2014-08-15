namespace KSPE3Lib
{
    public class HeaderText
    {
        public string Text { get; private set; }
        public E3Font Font { get; private set; }
        public double Offset { get; private set; }

        public HeaderText(string text, E3Font font, double offset)
        {
            Text = text;
            Font = font;
            Offset = offset;
        }
    }
}
