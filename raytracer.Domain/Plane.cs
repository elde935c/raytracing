using raytracer.Domain.Exceptions;


namespace raytracer.Domain;

public class Plane : Shape
{
    private Vector pointOnPlane;
    private Vector normal;

    public Plane(Vector pointOnPlane, Vector normal, 
        double diffusionConstant, bool refracts, 
        double refractionIndex)
        : base(diffusionConstant, refracts, refractionIndex)
    {
        this.pointOnPlane = pointOnPlane;
        this.normal = normal / normal.norm() ;
    }

    public Plane() : base(1, false, 1)
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

        double t = - Vector.dot(normal, lineStart) /
            Vector.dot(normal, lineDirection);

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