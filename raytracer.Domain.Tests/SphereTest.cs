namespace raytracer.Domain.Tests;
using Xunit;
using raytracer.Domain;

public class SphereTest
{
    [Fact]
    public void lineAlongZaxisThroughUnitSphere()
    {
        Sphere sphere = new Sphere();
        Line line = new Line(new([0, 0, 3]), new([0, 0, -1]));

        //todo finish this test
    }
}