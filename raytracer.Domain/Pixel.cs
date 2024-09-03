using System.Drawing;

namespace raytracer.Domain
{
    public class Pixel
    {
        private Color color;
        public Pixel()
        {        }

        public void SetRgb(double brightness)
        {
            // brightness \in (0,1)

            int red = (int)(brightness * 0xFF);
            int green = (int)(brightness * 0xFF);
            int blue = (int)(brightness * 0xFF);

            color = Color.FromArgb(red, green, blue);
        }

        public Color getRGB() { return color; }
    }
}
