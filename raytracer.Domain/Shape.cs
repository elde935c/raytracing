using System.Runtime.InteropServices.Marshalling;

namespace raytracer.Domain;

abstract public class Shape
{
    protected double diffusionConstant;
    public Shape(double diffusionConstant)
    {
        this.diffusionConstant = diffusionConstant;
    }
    
    public virtual Intersection intersect(Line line) { return null; }

    protected virtual Vector getNormal(Vector v) { return null; }

    protected virtual Boolean pointIsOnShape(Vector point) { return false; }

    public virtual double getDiffusionConstant(Line line) { return 0.0; }
}