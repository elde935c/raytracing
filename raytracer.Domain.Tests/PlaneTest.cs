namespace raytracer.Domain.Tests;
using Xunit;
using raytracer.Domain;
using raytracer.Domain.Exceptions;
using System.Globalization;
using System.Drawing;

public class PlaneTest
{
    Color color = Color.White;

    [Fact]
    public void lineStraightAtPlaneThroughOrigin()
    {
        Shape plane = new Plane();
        Line line = new Line(new([1, 0, 0]), new([ -1, 0, 0]));

        Intersection intersection = plane.intersect(line);

        Intersection solution = new Intersection(new([0,0,0]), plane);

        Assert.Equal(solution, intersection);
    }

    [Fact]
    public void lineStraightAtEquivalentPlaneThroughOrigin()
    {
        Shape plane = new Plane(new([0, 1, 1]), new([1, 0, 0]),
            1, true, 1, color);
        Line line = new Line(new([1, 0, 0]), new([-1, 0, 0]));

        Intersection intersection = plane.intersect(line);

        Intersection solution = new Intersection(new([0, 0, 0]), plane);

        Assert.Equal(solution, intersection);
    }

    [Fact]
    public void lineStraightAtPlaneBehindOrigin()
    {
        Shape plane = new Plane(new([-1, 1, 1]), new([1, 0, 0]), 1, true, 1, color);
        Line line = new Line(new([1, 0, 0]), new([-1, 0, 0]));

        Intersection intersection = plane.intersect(line);

        Intersection solution = new Intersection(new([-1, 0, 0]), plane);

        Assert.Equal(solution, intersection);
    }

    [Fact]
    public void lineThroughPlane()
    {
        Shape plane = new Plane();
        Line line = new Line(new([1, 0, 0]), new([-2, -2, 0]));

        Intersection intersection = plane.intersect(line);

        Intersection solution = new Intersection(new([0, -1, 0]), plane);

        Assert.Equal(solution, intersection);
    }


    [Fact]
    public void testIfTwoIdenticalPlanesAreTheSame()
    {
        Plane plane1 = new Plane();
        Plane plane2 = new Plane();

        Assert.Equal(plane1, plane2);
    }

    [Fact]
    public void testIfTwoDifferentPlanesAreUnequalPointInPlane()
    {
        Plane plane1 = new Plane();
        Plane plane2 = new Plane(new([1, 0, 0]), new([1, 0, 0]), 1, true, 1, color);

        Assert.NotEqual(plane1, plane2);
    }

    [Fact]
    public void testIfTwoDifferentPlanesAreUnequalNormal()
    {
        Plane plane1 = new Plane();
        Plane plane2 = new Plane(new([0, 0, 0]), new([0, 1, 0]),
            1, true, 1, color);

        Assert.NotEqual(plane1, plane2);
    }

    [Fact]
    public void testIfTwoDifferentPlanesAreUnequalRefraction()
    {
        Plane plane1 = new Plane();
        Plane plane2 = new Plane(new([0, 0, 0]), new([0, 1, 0]),
            1, false, 1, color);

        Assert.NotEqual(plane1, plane2);
    }

    [Fact]
    public void testRefraction()
    {
        Plane plane = new Plane(new([0, 0, 0]), new([1, 0, 0]), 1, true, 1.5, color);
        Line line = new Line(new([1, 0, 0]), new([-1, -1, 0]));
        Vector intersection = new([0,-1,0]);

        Line refractedLine = plane.getRefraction(intersection, line);

        double y = - Math.Sin(Math.PI/4) / 1.5;
        double x = - Math.Sqrt(1 - Math.Pow(y, 2));
        Line solution = new Line(intersection, new([x, y, 0]));

        Assert.Equal(solution,refractedLine);
    }

    
}