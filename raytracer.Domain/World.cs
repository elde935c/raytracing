using System.Drawing;

namespace raytracer.Domain;

public class World 
{
    // the refraction index of the air is assumed to be 1
    private readonly List<Shape> shapes;
    private readonly LightSource lightSource;
    private readonly MyColor backgroundColor;

    public World(List<Shape> shapes, 
        LightSource lightsource, 
        MyColor backgroundColor)
    {
        this.shapes = shapes;
        this.lightSource = lightsource;
        this.backgroundColor = backgroundColor;
    }

    private Intersection getClosestIntersection(Line line,
        bool includeRefractingObjects)
    {
        Intersection closest = null;
        foreach (Shape shape in shapes)
        {
            if (includeRefractingObjects || (!shape.getRefracts()))
            {
                Intersection nextIntersection = shape.intersect(line);
                closest = updateClosestIntersection(
                    closest, nextIntersection, line);
                
            }
        }
        if (closest != null && closest.getShape().getRefracts())
            return getClosestIntersection(closest.getRefractedLine(line), true);
        return closest;
    }


    private Intersection updateClosestIntersection(
        Intersection closest, Intersection nextIntersection, Line line)
    {
        if (nextIntersection != null)
        {
            if (closest == null) closest = nextIntersection;
            else
            {
                closest = returnClosest(closest, nextIntersection, line);
            }
        }
        return closest;
    }



    private Intersection returnClosest(Intersection i1,
        Intersection i2, Line line)
    {
        double distance1 = (i1.getCoord() - line.getStart()).norm();
        double distance2 = (i2.getCoord() - line.getStart()).norm();

        if (distance1 < distance2) return i1;
        return i2;
    }

    private MyColor calcColorAtIntersection(Intersection intersection,
        Line inComingLine)
    {
        Vector intersectionCoord = intersection.getCoord();
        Vector intersectionToLightDirection = lightSource.getCoord() - 
            intersectionCoord;
        Line intersectionToLight = new Line(intersectionCoord,
            intersectionToLightDirection);

        if (intersection.getShape().shapeBlocksLight(
            inComingLine.getDirection(),
            intersectionCoord, intersectionToLightDirection))
        { // shape itself blocks light 
            return MyColor.Black;
        }

        // exclude refracting objects, they do not cast a shadow
        // it will be too complicated otherwise
        Intersection closest = 
            getClosestIntersection(intersectionToLight, true);

        if (closest == null)
        { // not blocked by other shape
            return intersection.getShape().getColorFromLine(
                intersectionToLight);
        }
        else if ((intersectionCoord - closest.getCoord()).norm() > 
                intersectionToLightDirection.norm()) 
        { // lightsource is between other shape and intersection
            return intersection.getShape().getColorFromLine(
                intersectionToLight);
        }

        return MyColor.Black;
    }

    public MyColor getColorAtIntersection(Line line)
    {
        Intersection intersection = getClosestIntersection(line, true);
        if (intersection == null)
        {
            return backgroundColor;
        }
        return calcColorAtIntersection(intersection, line);
    }


    internal protected Intersection getClosestIntersectionWrapper(Line line)
    {
        return getClosestIntersection(line, true);
    }

    internal protected MyColor calcColorAtIntersectionWrapper(
        Intersection intersection, Line line)
    {
        return calcColorAtIntersection(intersection, line);
    }
}

