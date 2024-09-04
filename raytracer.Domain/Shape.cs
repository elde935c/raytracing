using raytracer.Domain.Exceptions;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

namespace raytracer.Domain;

abstract public class Shape
{
    protected double diffusionConstant;
    protected Boolean refracts;
    protected double refractionIndex;

    public Shape(double diffusionConstant, Boolean refracts,
        double refractionIndex)
    {
        this.diffusionConstant = diffusionConstant;
        this.refracts = refracts;
        this.refractionIndex = refractionIndex;
    }
    
    public virtual Intersection intersect(Line line) { return null; }

    protected virtual Vector getNormal(Vector v) { return null; }


    protected virtual Boolean pointIsOnShape(Vector point) { return false; }

    public double getDiffusionConstantFromLine(Line ln)
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
            + " is not on shape with center");
    }

    public Boolean getRefracts() { return refracts; }

    public double getRefractionIndex() { return refractionIndex; }

    public double getDiffusionConstant() { return diffusionConstant; }

    public Line getRefraction(Vector intersectionCoord, Line line)
    {
        Vector lineDirection = line.getDirection();
        lineDirection = lineDirection / lineDirection.norm();
        Vector normal = getNormal(intersectionCoord);

        double dotProduct = - Vector.dot(lineDirection, normal);

        Vector refractionDirection = lineDirection / refractionIndex +
            (dotProduct / refractionIndex -
            Math.Sqrt(1.0 - 1.0 / Math.Pow(refractionIndex, 2) * (1 - Math.Pow(dotProduct, 2)))) *
            normal;

        return new Line(intersectionCoord, refractionDirection);
    }


    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        double refractionIndexOther = ((Shape)obj).getRefractionIndex();
        Boolean refractsOther = ((Shape)obj).getRefracts();
        double diffusionConstantOther = ((Shape)obj).getDiffusionConstant();

        return Math.Abs(refractionIndex - refractionIndexOther) < 1e-9 &&
            refracts == refractsOther &&
            Math.Abs(diffusionConstant - diffusionConstantOther) < 1e-9;
    }

    public override int GetHashCode()
    {
        return refractionIndex.GetHashCode() + 
            refracts.GetHashCode() + 
            diffusionConstant.GetHashCode();
    }
}