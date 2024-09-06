using raytracer.Domain.Exceptions;
using System.Drawing;


namespace raytracer.Domain;

public class Plane : Shape
{
    protected Vector pointOnPlane;
    protected Vector normal;

    public Plane(Vector pointOnPlane, Vector normal,
        double diffusionConstant, bool refracts,
        double refractionIndex, MyColor color)
        : base(diffusionConstant, refracts, refractionIndex, color)
    {
        this.pointOnPlane = pointOnPlane;
        this.normal = normal / normal.norm() ;
    }

    public Plane() : base(1, false, 1, MyColor.White)
    {
        this.pointOnPlane = new([0,0,0]);
        this.normal = new([1, 0, 0]);
    }

    public override Intersection intersect(Line line)
    {
        Vector lineStart = line.getStart();
        Vector startToPointOnPlane = pointOnPlane - lineStart;
        Vector lineDirection = line.getDirection();

        // line goes away from the plane or is parallel
        if (Vector.dot(startToPointOnPlane, lineDirection) <= 0) 
            return null;

        double t = - Vector.dot(normal, (lineStart-pointOnPlane)) /
            Vector.dot(normal, lineDirection);
        if (t<1e-9)
        { //lineStart is on the plane,
          //or intersection at plane is in opposite direction
            return null;
        }

        Vector intersectionCoord = lineStart + t * lineDirection;

        return new Intersection(intersectionCoord, this);
    }

    protected override Vector getNormal(Vector v)
    {
        return normal;
    }

    protected override bool pointIsOnShape(Vector point)
    {
        Vector pointToPointOnPlane = pointOnPlane - point;
        return Math.Abs(Vector.dot(normal, pointToPointOnPlane)) < 1e-8; 
    }

    public Vector getPointOnPlane() { return pointOnPlane; }

    public Vector getNormal() { return normal; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Vector pointOnPlaneOther = ((Plane)obj).getPointOnPlane();
        Vector normalOther = ((Plane)obj).getNormal();

        return pointOnPlane.Equals(pointOnPlaneOther) &&
            normal.Equals(normalOther) &&
            base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return pointOnPlane.GetHashCode() + normal.GetHashCode() +
            base.GetHashCode();
    }
}