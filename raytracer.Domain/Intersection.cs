namespace raytracer.Domain;

public class Intersection
{
    private Vector coord;
    private Shape shape;

    public Intersection(Vector coord, Shape shape)
    {
        this.coord = coord;
        this.shape = shape;
    }

    public Vector getCoord() { return coord; }
}

//todo write Equals and hashcode