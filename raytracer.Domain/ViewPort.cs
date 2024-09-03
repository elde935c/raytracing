using System;
using System.Drawing;
using System.Drawing.Imaging;

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
    }

    private Vector getCoordinate(int i)
    {
        double x = (i % screenWidth) + 0.5;
        double y = ((int)i / screenHeight) + 0.5;
        return x * (corners[1] - corners[0]) - y*(corners[2] - corners[1]); 
    }

    private Line getLine(int i)
    {
        return new Line(viewPoint, getCoordinate(i) - viewPoint);
    }

    public void createImage()
    {
        for (int i = 0; i<screenHeight*screenWidth; i++)
        {
            pixels[i].SetRgb(scene.GetWorld().getBrightness(getLine(i)));
        }

        // Create a bitmap from the RGB values
        Bitmap image = CreateBitmapFromRGBArray();

        // Save the bitmap as an image file
        image.Save("output.png", System.Drawing.Imaging.ImageFormat.Png);
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
