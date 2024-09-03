using raytracer.Domain.Exceptions;


namespace raytracer.Domain;

public class Sphere : Shape
{
    private Vector center;
    private double radius;

    public Sphere(Vector center, double radius, double diffusionConstant)
        : base(diffusionConstant)
    {
        this.center = center;
        this.radius = radius;
    }

    public Sphere() : base(1)
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
            if (t0 > t1 && t1 > 1e-9)
            {
                return new Intersection(lineStart + 
                    (t1*lineDirection), this);
            }
            else if (t1 > t0 && t0 > 1e-9)
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

    public override double getDiffusionConstant(Line ln)
    {
        Vector lineStart = ln.getStart();
        if (pointIsOnShape(lineStart))
        {
            Vector lineDirection = ln.getDirection();
            // diffusion coefficient is based on the angle between the normal of the shape and the line from the
            return this.diffusionConstant * Math.Max(0,
                Vector.dot(getNormal(ln.getStart()), lineDirection)
                / lineDirection.norm());
        }
        else throw new PointNotOnShapeException(lineStart.ToString() 
            + " is not on sphere with center " + this.center.ToString +
            " and radius " + this.radius);
    }
}