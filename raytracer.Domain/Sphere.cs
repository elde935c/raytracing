namespace raytracer.Domain;

public class Sphere : Shape
{
    private Vector center;
    private double radius;

    public Sphere(Vector center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }

    public Sphere()
    {
        this.center = new([0, 0, 0]);
        this.radius = 1;
    }

    
    public override Intersection intersect(Line line)
    {
        // use the abc-formula to determine if there is a solution
        Vector lineDirection = line.getDirection();
        Vector lineStartToCenter = line.getStart() - this.center;
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
                return new Intersection(line.getStart() + (t1*line.getDirection()), this);
            }
            else if (t1 > t0 && t0 > 1e-9)
            {
                return new Intersection(line.getStart() + (t0 * line.getDirection()), this);
            }
        }
        return null;
    }
}