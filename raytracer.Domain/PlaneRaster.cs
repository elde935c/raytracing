using System.Drawing;
using System.Net.Mail;

namespace raytracer.Domain;

public class PlaneRaster : Plane {

    private MyColor lineColor;
    private Vector horizontalAxis;
    private Vector verticalAxis;
    private double lineWidth;
    private double rasterWidth;

    public PlaneRaster(Vector pointOnPlane, Vector normal,
        double diffusionConstant, bool refracts,
        double refractionIndex, MyColor color,
        Vector horizontalAxis, MyColor LineColor,
        double lineWidth, double rasterWidth)

        : base(pointOnPlane, normal, diffusionConstant, false,
        refractionIndex, color)
    {
        this.lineColor = LineColor;
        this.horizontalAxis = horizontalAxis / horizontalAxis.norm();
        this.verticalAxis = Vector.crossProduct(horizontalAxis, normal);
        this.verticalAxis = this.verticalAxis / this.verticalAxis.norm();
        this.lineWidth = lineWidth;
        this.rasterWidth = rasterWidth;
    }

    public PlaneRaster() : base()
    {
        this.lineColor = MyColor.Black;
        this.horizontalAxis = new([0, 0, 1]);
        this.verticalAxis = new([0, 1, 0]);
        this.lineWidth = 0.1;
        this.rasterWidth = 1;
    }

    public override MyColor getColorFromLine(Line ln)
    {
        double brightness = getDiffusionConstantFromLine(ln);

        return brightness * 
            (pointIsOnGridLine(ln.getStart()) ? lineColor : color);
    }

    private Boolean pointIsOnGridLine(Vector point)
    {
        double t1 = Vector.dot((point - pointOnPlane), horizontalAxis);
        double t2 = Vector.dot((point - pointOnPlane), verticalAxis);

        return (Math.Abs(t1 % rasterWidth) < lineWidth) ||
            (Math.Abs(t2 % rasterWidth) < lineWidth);
    }
}