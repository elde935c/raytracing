namespace raytracer.Domain.Tests;
using Xunit;
using raytracer.Domain;
using raytracer.Domain.Exceptions;

public class SphereTest
{
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


    public void testDiffusionConstantWithPointNotOnSphere()
    {
        Sphere sphere = new Sphere(new([1, 0, 0]), 2, 1);
        Vector normalAt = new([1, 2, 2]);
        Vector direction = new([0, 1, 0]);
        Line line = new Line(normalAt, direction);

        Assert.Throws<PointNotOnShapeException>(() =>
            sphere.getDiffusionConstant(line));
    }

    public void testDiffusionConstantPerpendicularLineShouldReturnOne()
    {
        Sphere sphere = new Sphere();
        Vector normalAt = new([1, 0, 0]);
        Vector direction = new([1, 0, 0]);
        Line line = new Line(normalAt, direction);

        Assert.True(Math.Abs(sphere.getDiffusionConstant(line) - 1) < 1e-5);
    }

    public void testDiffusionConstantParallelLineShouldReturnZero()
    {
        Sphere sphere = new Sphere();
        Vector normalAt = new([1, 0, 0]);
        Vector direction = new([0, 1, 0]);
        Line line = new Line(normalAt, direction);

        Assert.True(Math.Abs(sphere.getDiffusionConstant(line)) < 1e-5);
    }

    public void testDiffusionConstantLineBehindSphereSurfaceShouldReturnZero()
    {
        Sphere sphere = new Sphere();
        Vector normalAt = new([1, 0, 0]);
        Vector direction = new([-1, -1, 0]);
        Line line = new Line(normalAt, direction);

        Assert.True(Math.Abs(sphere.getDiffusionConstant(line)) < 1e-5);
    }

}