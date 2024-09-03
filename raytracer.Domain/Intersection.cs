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
}

