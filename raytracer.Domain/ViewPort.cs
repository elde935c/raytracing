using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Threading.Tasks;

namespace raytracer.Domain;

public class ViewPort
{
    private Vector[] corners;
    private Vector viewPoint;
    private Pixel[] pixels;
    private readonly int screenWidth;
    private readonly int screenHeight;
    private readonly Scene scene;

    public ViewPort(Vector[] corners, Vector viewPoint,
        int screenWidth, int screenHeight, Scene scene)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;

        this.viewPoint = viewPoint;
        this.corners = corners;

        this.scene = scene;

        pixels = new Pixel[screenWidth * screenHeight];
        for (int i = 0; i < screenWidth * screenHeight; i++) pixels[i] = new Pixel();
    }

    private Vector getCoordinate(int i)
    {
        // x,y \in (0,1)
        double x = ((i % screenWidth) + 0.5)/screenWidth;
        double y = (Math.Floor(i / screenWidth + 0.0) + 0.5) / screenHeight;
        return corners[0] + x * (corners[1] - corners[0]) +
            y*(corners[2] - corners[0]); 
    }

    private Line getLine(int i)
    {
        return new Line(viewPoint, getCoordinate(i) - viewPoint);
    }

    public void createImage(string imageName, Boolean parallel)
    {
        if (parallel)
        {
            Parallel.For(0, screenWidth * screenHeight, i =>
            {
                pixels[i].SetRgb(scene.GetWorld().getBrightness(getLine(i)));
            });
        }

        else
        {
            for (int i=0; i<screenHeight*screenWidth; i++)
            {
                pixels[i].SetRgb(scene.GetWorld().getBrightness(getLine(i)));
            }
        }

        // Create a bitmap from the RGB values
        Bitmap image = CreateBitmapFromRGBArray();

        string dir = System.IO.Directory.GetCurrentDirectory();
        dir = dir.Substring(0, dir.IndexOf("bin"));
        dir = dir.Replace("\\", "/");

        // Save the bitmap as an image file
        image.Save(dir + "/output/" + imageName, System.Drawing.Imaging.ImageFormat.Png);
    }

    private Bitmap CreateBitmapFromRGBArray()
    {
        Bitmap image = new Bitmap(screenWidth, screenHeight,
            System.Drawing.Imaging.PixelFormat.Format24bppRgb);

        for (int i=0; i<screenWidth*screenHeight; i++)
        {
            int x = i % screenWidth;
            int y = i / screenHeight;
            image.SetPixel(x, y, pixels[i].getRGB());
        }
        return image;
    }
   
}
