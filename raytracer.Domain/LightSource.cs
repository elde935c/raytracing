namespace raytracer.Domain;

public class LightSource
{
    private readonly Vector coord;
    private readonly double brightNess;

    public LightSource(Vector coord, double brightNess)
    {
        this.coord = coord;
        this.brightNess = brightNess;
    }   

    public Vector getCoord() { return coord; } 

    public double getBrightNess() { return brightNess; }
}
