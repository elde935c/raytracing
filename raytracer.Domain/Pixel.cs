using System.Drawing;

namespace raytracer.Domain
{
    public class Pixel
    {
        private Color color;
        public Pixel()
        {        }

        public void SetRgb(Color color)
        {
            this.color = color;
        }

        public Color getRGB() { return color; }
    }
}
