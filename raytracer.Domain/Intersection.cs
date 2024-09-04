namespace raytracer.Domain;

public class Intersection
{
    private readonly Vector coord;
    private readonly Shape shape;

    public Intersection(Vector coord, Shape shape)
    {
        this.coord = coord;
        this.shape = shape;
    }

    public Vector getCoord() { return coord; }

    public Shape getShape() { return shape; }

    public Line getRefractedLine(Line line)
    {
        return shape.getRefraction(coord, line);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Vector coordOther = ((Intersection)obj).getCoord();
        Shape shapeOther = ((Intersection)obj).getShape();

        return coord.Equals(coordOther) && shape.Equals(shapeOther);
    }

    public override int GetHashCode()
    {
        return coord.GetHashCode() + shape.GetHashCode();
    }
}

