using System.Runtime.ConstrainedExecution;

namespace raytracer.Domain;

public class Line
{
    private readonly Vector start;
    private readonly Vector direction;

    public Line(Vector start, Vector direction)
    {
        this.start = start;
        this.direction = direction;
    }

    public Vector getStart() { return start; }

    public Vector getDirection() { return direction; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Vector startOther = ((Line)obj).getStart();
        Vector directionOther = ((Line)obj).getDirection();

        return start.Equals(startOther) &&
            direction.Equals(directionOther);
    }

    public override int GetHashCode()
    {
        return start.GetHashCode() + direction.GetHashCode();
    }
}
