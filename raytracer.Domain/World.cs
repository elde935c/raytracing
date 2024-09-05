using System.Drawing;

namespace raytracer.Domain;

public class World 
{
    // the refraction index of the air is assumed to be 1
    private readonly List<Shape> shapes;
    private readonly LightSource lightSource;
    private readonly Color backgroundColor = Color.DarkBlue;

    public World(List<Shape> shapes, LightSource lightsource, Color backgroundColor)
    {
        this.shapes = shapes;
        this.lightSource = lightsource;
        this.backgroundColor = backgroundColor;
    }

    private Intersection getClosestIntersection(Line line) 
    {
        Intersection closest = null;
        foreach (Shape shape in shapes)
        {
            Intersection nextIntersection = shape.intersect(line);
            if (nextIntersection != null)
            {
                if (closest == null) closest = nextIntersection;
                else
                {
                    closest = returnClosest(closest,
                        nextIntersection, line);
                }
            }
        }
        if (closest != null && closest.getShape().getRefracts())
            return getClosestIntersection(closest.getRefractedLine(line));
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

    private Color calcColorAtIntersection(Intersection intersection) 
    {
        Vector intersectionCoord = intersection.getCoord();
        Line intersectionToLight = new Line(intersectionCoord,
            lightSource.getCoord() - intersectionCoord);

        if (getClosestIntersection(intersectionToLight) == null)
            return intersection.getShape().getColorFromLine(
                intersectionToLight);
        return backgroundColor;
    }

    public Color getColorAtIntersection(Line line)
    {
        Intersection intersection = getClosestIntersection(line);
        if (intersection==null) return backgroundColor;
        return calcColorAtIntersection(intersection);
    }


    internal protected Intersection getClosestIntersectionWrapper(Line line)
    {
        return getClosestIntersection(line);
    }

    internal protected Color calcColorAtIntersectionWrapper(Intersection intersection)
    {
        return calcColorAtIntersection(intersection);
    }
}

