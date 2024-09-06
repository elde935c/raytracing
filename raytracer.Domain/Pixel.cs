using System.Drawing;

namespace raytracer.Domain
{
    public class Pixel
    {
        private MyColor color;
        public Pixel()
        {        }

        public void SetColor(MyColor color)
        {
            this.color = color;
        }

        public Color getColor() 
        {
            return Color.FromArgb(color.R, color.G, color.B); ;
        }
    }
}
