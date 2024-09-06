namespace raytracer.Domain.Tests;
using Xunit;
using raytracer.Domain;
using raytracer.Domain.Exceptions;
using System.Drawing;

public class SphereTest
{
    MyColor color = MyColor.White;

    [Fact]
    public void lineAlongZaxisThroughUnitSphere()
    {
        Sphere sphere = new Sphere();
        Line line = new Line(new([0, 0, 3]), new([0, 0, -1]));

        Intersection intersection = sphere.intersect(line);
        Vector intersectionCoord = intersection.getCoord();

        Vector solution = new([0,0,1]);

        Assert.Equal(solution, intersectionCoord);
    }


    [Fact]
    public void lineNotIntersectingWithSphereShouldReturnNull()
    {
        Sphere sphere = new Sphere();
        Line line = new Line(new([0, 0, 3]), new([0, 0, 1]));

        Intersection intersection = sphere.intersect(line);

        Assert.Null(intersection);
    }


    [Fact]
    public void testIfTwoIdenticalSpheresAreTheSame()
    {
        Sphere sphere1 = new Sphere();
        Sphere sphere2 = new Sphere();

        Assert.Equal(sphere1, sphere2);
    }

    [Fact]
    public void testIfTwoDifferentSpheresAreUnequal()
    {
        Sphere sphere1 = new Sphere();
        Sphere sphere2 = new Sphere(new([1, 0, 0]), 1, 1, true, 1, color);

        Assert.NotEqual(sphere1, sphere2);
    }

    [Fact]
    public void testIfTwoDifferentRadiusSpheresAreUnequal()
    {
        Sphere sphere1 = new Sphere();
        Sphere sphere2 = new Sphere(new([0, 0, 0]), 2, 1, true, 1, color);

        Assert.NotEqual(sphere1, sphere2);
    }

}