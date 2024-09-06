using raytracer.Domain.Exceptions;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

namespace raytracer.Domain;

abstract public class Shape
{
    protected double diffusionConstant;
    protected Boolean refracts;
    protected double refractionIndex;
    protected MyColor color;

    public Shape(double diffusionConstant, Boolean refracts,
        double refractionIndex, MyColor color)
    {
        this.diffusionConstant = diffusionConstant;
        this.refracts = refracts;
        this.refractionIndex = refractionIndex;
        this.color = color;
    }
    
    public virtual Intersection intersect(Line line) { return null; }

    protected virtual Vector getNormal(Vector v) { return null; }


    protected virtual Boolean pointIsOnShape(Vector point) { return false; }

    protected double getDiffusionConstantFromLine(Line ln) //todo make private
    {
        Vector lineStart = ln.getStart();
        if (pointIsOnShape(lineStart))
        {
            Vector lineDirection = ln.getDirection();
            // diffusion coefficient is based on the angle
            // between the normal of the shape and the line from the

            return this.diffusionConstant * Math.Abs(
                Vector.dot(getNormal(ln.getStart()), lineDirection)
                / lineDirection.norm());
        }
        else throw new PointNotOnShapeException(lineStart.ToString()
            + " is not on shape with center");
    }

    public virtual MyColor getColorFromLine(Line ln)
    {
        double brightness = getDiffusionConstantFromLine(ln);

        return brightness * color;
    }

    public Boolean getRefracts() { return refracts; }

    public double getRefractionIndex() { return refractionIndex; }

    public double getDiffusionConstant() { return diffusionConstant; }

    public Line getRefraction(Vector intersectionCoord, Line line)
    {
        Vector lineDirection = line.getDirection();
        lineDirection = lineDirection / lineDirection.norm();
        Vector normal = getNormal(intersectionCoord);

        double currentRefractionIndex = refractionIndex;

        double dotProduct = Vector.dot(lineDirection, normal);
        Vector temporaryNormal = normal;
        if (dotProduct > 0)
        {  // meaning we are in an object
            currentRefractionIndex = 1 / refractionIndex;
            dotProduct = -dotProduct; // swap direction of the normal for this calculation
            temporaryNormal = -1.0* normal;
        }

        double rootArgument = 1.0 - 1.0 / Math.Pow(currentRefractionIndex, 2) *
            (1 - Math.Pow(dotProduct, 2));

        Vector refractionDirection;
        if (rootArgument < 0) 
        { //reflect
            refractionDirection = lineDirection - 
                2.0 * dotProduct * temporaryNormal;
        }
        else
        { //refract
            refractionDirection = lineDirection / currentRefractionIndex +
            (-dotProduct / currentRefractionIndex - 
            Math.Sqrt(rootArgument)) * temporaryNormal;
        }
        

        return new Line(intersectionCoord, refractionDirection);
    }


    public Boolean shapeBlocksLight(Vector directionIn, Vector intersectCoord,
        Vector directionOut)
    {
        Vector normalAt = getNormal(intersectCoord);
        return Vector.dot(directionIn, normalAt) *
            Vector.dot(directionOut, normalAt) > 0;
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

    internal protected double getDiffusionConstantFromLineWrapper(Line line)
    {
        return getDiffusionConstantFromLine(line);
    }
}