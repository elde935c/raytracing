namespace raytracer.Domain;

abstract public class Shape
{
    public virtual Intersection intersect(Line line) { return null; }


}