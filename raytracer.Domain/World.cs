namespace raytracer.Domain;

public class World 
{
    private readonly List<Shape> shapes;
    private readonly LightSource lightSource;

    public World (List<Shape> shapes, LightSource lightsource)
    {
        this.shapes = shapes;
        this.lightSource = lightsource;
    }

    private Intersection getClosestIntersection(Line line) //todo make private
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

    private double calcBrightness(Intersection intersection) 
    {
        Vector intersectionCoord = intersection.getCoord();
        Line intersectionToLight = new Line(intersectionCoord,
            lightSource.getCoord() - intersectionCoord);

        if (getClosestIntersection(intersectionToLight) == null)
            return intersection.getShape().getDiffusionConstantFromLine(
                intersectionToLight);
        return 0.0;
    }

    public double getBrightness(Line line)
    {
        Intersection intersection = getClosestIntersection(line);
        if (intersection==null) return 0.0;
        return calcBrightness(intersection);
    }


    internal protected Intersection getClosestIntersectionWrapper(Line line)
    {
        return getClosestIntersection(line);
    }

    internal protected double calcBrightnessWrapper(Intersection intersection)
    {
        return calcBrightness(intersection);
    }
}

