using Xunit;
using raytracer.Domain;
using raytracer.Domain.Exceptions;
using System.Drawing;

namespace raytracer.Domain.Tests;

public class ShapeTest
{
    MyColor color = MyColor.White;

    [Fact]
    public void testDiffusionConstantWithPointNotOnSphere()
    {
        Boolean refracts = false;
        double refractionIndex = 1;
        Sphere sphere = new Sphere(new([1, 0, 0]), 2, 1, refracts, refractionIndex, color);
        Vector normalAt = new([1, 2, 2]);
        Vector direction = new([0, 1, 0]);
        Line line = new Line(normalAt, direction);

        Assert.Throws<PointNotOnShapeException>(() =>
            sphere.getDiffusionConstantFromLineWrapper(line));
    }

    [Fact]
    public void testDiffusionConstantPerpendicularLineShouldReturnOne()
    {
        Sphere sphere = new Sphere();
        Vector normalAt = new([1, 0, 0]);
        Vector direction = new([1, 0, 0]);
        Line line = new Line(normalAt, direction);

        Assert.True(Math.Abs(sphere.getDiffusionConstantFromLineWrapper(line) - 1) < 1e-5);
    }

    [Fact]
    public void testDiffusionConstantParallelLineShouldReturnZero()
    {
        Sphere sphere = new Sphere();
        Vector normalAt = new([1, 0, 0]);
        Vector direction = new([0, 1, 0]);
        Line line = new Line(normalAt, direction);

        Assert.True(Math.Abs(sphere.getDiffusionConstantFromLineWrapper(line)) < 1e-5);
    }


    [Fact]
    public void testRefractionStraightAtSphereShouldHaveSameDirection()
    {
        Sphere sphere = new Sphere();
        Vector intersection = new([1, 0, 0]);
        Vector direction = new([-1, 0, 0]);
        Vector start = new([2, 0, 0]);
        Line line = new Line(start, direction);

        Line newLine = sphere.getRefraction(intersection, line);

        Line solution = new Line(intersection, direction);

        Assert.Equal(solution, newLine);
    }

    [Fact]
    public void testColorConstantWithPointNotOnSphere()
    {
        Boolean refracts = false;
        double refractionIndex = 1;
        Sphere sphere = new Sphere(new([1, 0, 0]), 2, 1, 
            refracts, refractionIndex, color);
        Vector normalAt = new([1, 2, 2]);
        Vector direction = new([0, 1, 0]);
        Line line = new Line(normalAt, direction);

        Assert.Throws<PointNotOnShapeException>(() =>
            sphere.getColorFromLine(line));
    }

    [Fact]
    public void testColorPerpendicularLineShouldReturnWhite()
    {
        Sphere sphere = new Sphere();
        Vector normalAt = new([1, 0, 0]);
        Vector direction = new([1, 0, 0]);
        Line line = new Line(normalAt, direction);

        Assert.Equal(MyColor.White, sphere.getColorFromLine(line));
    }

    [Fact]
    public void testColorParallelLineShouldReturnBlack()
    {
        Sphere sphere = new Sphere();
        Vector normalAt = new([1, 0, 0]);
        Vector direction = new([0, 1, 0]);
        Line line = new Line(normalAt, direction);

        Assert.Equal(MyColor.Black, sphere.getColorFromLine(line));
    }
}

