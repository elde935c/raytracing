using raytracer.Domain.Exceptions;
using System.Drawing;


namespace raytracer.Domain;

public class Sphere : Shape
{
    private Vector center;
    private double radius;

    public Sphere(Vector center, double radius,
        double diffusionConstant, bool refracts,
        double refractionIndex, MyColor color)
        : base(diffusionConstant, refracts, refractionIndex, color)
    {
        this.center = center;
        this.radius = radius;
    }

    public Sphere() : base(1, false, 1, MyColor.White)
    {
        this.center = new([0, 0, 0]);
        this.radius = 1;
    }

    
    public override Intersection intersect(Line line)
    {
        // use the abc-formula to determine if there is a solution
        Vector lineDirection = line.getDirection();
        Vector lineStart = line.getStart();
        Vector lineStartToCenter = lineStart - this.center;
        double A = Vector.dot(lineDirection, lineDirection);
        double B = 2 * Vector.dot(lineStartToCenter, lineDirection);
        double C = Vector.dot(lineStartToCenter, lineStartToCenter) -
            Math.Pow(this.radius, 2);

        double Discriminant = B * B - 4.0f * A * C;

        if (Discriminant > 0)
        {
            double t0 = (-B - Math.Sqrt(Discriminant)) / 2 / A;
            double t1 = (-B + Math.Sqrt(Discriminant)) / 2 / A;
            if (t1>1e-9 && (t0<1e-9 || t0>t1))
            { // t1>0 for the right direction, t0 must be either negative or larger than t1
                return new Intersection(lineStart + 
                    (t1*lineDirection), this);
            }
            else if (t0 > 1e-9 && (t1 < 1e-9 || t1 > t0))
            {
                return new Intersection(lineStart + 
                    (t0 * lineDirection), this);
            }
        }
        return null;
    }

    protected override Vector getNormal(Vector v)
    {
        return (v - this.center) / this.radius;
    }

    protected override bool pointIsOnShape(Vector point)
    {
        return Math.Abs((this.center - point).norm()-this.radius) < 1e-8;
    }

    public Vector getCenter() { return this.center; }

    public double getRadius() { return this.radius; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Vector centerOther = ((Sphere)obj).getCenter();
        double radiusOther = ((Sphere)obj).getRadius();

        return center.Equals(centerOther) &&
            Math.Abs(radiusOther - this.radius) < 1e-9 &&
            base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return center.GetHashCode() + radius.GetHashCode() +
            base.GetHashCode();
    }
}