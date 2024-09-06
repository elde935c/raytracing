using Xunit;
using raytracer.Domain;
using System.Drawing;


namespace raytracer.Domain.Tests;

public class WorldTest
{
    Boolean refracts = false;
    double refractionIndex = 1;
    MyColor color = MyColor.White;
    MyColor backgroundColor = MyColor.DarkBlue;
    List<Shape> shapes;

    public WorldTest() 
    {
        shapes = new List<Shape>();
    }


    [Fact]
    public void testIfClosestIntersectionIsReturnedOneSphere()
    {
        shapes.Add(new Sphere());
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

        Vector solution = new([1, 0, 0]);

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Equal(solution, intersection.getCoord());
    }

    [Fact]
    public void testIfClosestIntersectionIsReturnedTwoSpheres()
    {
        shapes.Add(new Sphere());
        shapes.Add(new Sphere(new([-3, 0, 0]), 1, 1, refracts, refractionIndex, color));
        World world = new World(shapes,
            new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

        Vector solution = new([1, 0, 0]);

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Equal(solution, intersection.getCoord());
    }

    [Fact]
    public void testIfClosestIntersectionIsReturnedOverlappingSpheres()
    {
        shapes.Add(new Sphere());
        shapes.Add(new Sphere(new([-3, 0, 0]), 3, 1, refracts, refractionIndex, color));
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

        Vector solution = new([1, 0, 0]);

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Equal(solution, intersection.getCoord());
    }

    [Fact]
    public void testLineNotIntersectionShouldReturnNull()
    {
        shapes.Add(new Sphere());
        shapes.Add(new Sphere(new([-3, 0, 0]), 1, 1, refracts, refractionIndex, color));
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Line line = new Line(new([-1.5, -10, 0]), new([0, 1, 0]));

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Null(intersection);
    }

    [Fact]
    public void lineThroughLenseToNothing()
    {
        shapes.Add(new Sphere(new([-3, 0, 0]), 1, 1,
            true, refractionIndex, color));
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Line line = new Line(new([-10, 0, 0]), new([1, 0, 0]));

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Null(intersection);

    }

    [Fact]
    public void lineStraightThroughPlaneToSphere()
    {
        Sphere sphere = new Sphere();
        shapes.Add(sphere);
        shapes.Add(new Plane(new([-3, 0, 0]),
            new([1, 0, 0]), 1, true, 1.5, color));
        World world = new World(shapes,
            new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Line line = new Line(new([-10, 0, 0]), new([1, 0, 0]));

        Intersection intersection = 
            world.getClosestIntersectionWrapper(line);
        Intersection solution = new Intersection(new([-1, 0, 0]),
            sphere);

        Assert.Equal(solution, intersection);

    }

    [Fact]
    public void lineStraightThroughTwoSpheresToSphere()
    {
        Sphere sphere = new Sphere();
        shapes.Add(sphere);
        shapes.Add(new Sphere(new([-3, 0, 0]), 1, 1,
            true, refractionIndex, color));
        shapes.Add(new Sphere(new([-5, 0, 0]), 1, 1,
            true, refractionIndex, color));
        World world = new World(shapes,
            new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Line line = new Line(new([-10, 0, 0]), new([1, 0, 0]));

        Intersection intersection =
            world.getClosestIntersectionWrapper(line);
        Intersection solution = new Intersection(new([-1, 0, 0]),
            sphere);

        Assert.Equal(solution, intersection);

    }

    [Fact]
    public void boxedInPointOfView()
    {
        shapes.Add(new Plane(new([2, 0, 0]),
            new([-1, 0, 0]), 1, false, 1, color));
        shapes.Add(new Plane(new([-2, 0, 0]),
            new([1, 0, 0]), 1, false, 1, color));
        Plane solutionPlane = new Plane(new([0, 2, 0]),
            new([0, -1, 0]), 1, false, 1, color);
        shapes.Add(solutionPlane);
        shapes.Add(new Plane(new([0, -2, 0]),
            new([0, 1, 0]), 1, false, 1, color));
        shapes.Add(new Plane(new([0, 0, 2]),
            new([0, 0, 1]), 1, false, 1, color));
        shapes.Add(new Plane(new([0, 0, -2]),
            new([0, 0, 1]), 1, false, 1, color));
        World world = new World(shapes,
            new LightSource(new([0, 10, 0]), 1), backgroundColor);
        Line line = new Line(new([0.99, 0, 0]), new([0, 1, 0]));

        Intersection intersection =
            world.getClosestIntersectionWrapper(line);

        Intersection solution = new Intersection(new([0.99, 2, 0]),
            solutionPlane);

        Assert.Equal(solution, intersection);
    }


    //[Fact]
    //public void returnNullWhenTrappedInSphere()
    //{
    //    Vector center = new([0, 0, 0]);
    //    Sphere smallSphere = new Sphere(center, 1, 1, true,
    //        100, color);
    //    shapes.Add(smallSphere);
    //    shapes.Add(new Plane(new([2, 0, 0]),
    //        new([-1, 0, 0]), 1, false, 1, color));
    //    shapes.Add(new Plane(new([-2, 0, 0]),
    //        new([1, 0, 0]), 1, false, 1, color));
    //    shapes.Add(new Plane(new([0, 2, 0]),
    //        new([0, -1, 0]), 1, false, 1, color));
    //    shapes.Add(new Plane(new([0, -2, 0]),
    //        new([0, 1, 0]), 1, false, 1, color));
    //    shapes.Add(new Plane(new([0, 0, 2]),
    //        new([0, 0, 1]), 1, false, 1, color));
    //    shapes.Add(new Plane(new([0, 0, -2]),
    //        new([0, 0, 1]), 1, false, 1, color));
    //    World world = new World(shapes,
    //        new LightSource(new([0, 10, 0]), 1), backgroundColor);
    //    Line line = new Line(new([0.99, 0, 0]), new([0, 1, 0]));

    //    Intersection intersection =
    //        world.getClosestIntersectionWrapper(line);

    //    Assert.Null(intersection);
    //    //Assert.Fail();
    //}

    [Fact]
    public void getBrightnessWithSingleSphere()
    {
        shapes.Add(new Sphere());
        World world = new World(shapes, new LightSource(new([10, 0, 0]), 1), backgroundColor);

        Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Equal(MyColor.White,
            world.calcColorAtIntersectionWrapper(intersection, line));
    }

    [Fact]
    public void getBrightnessWithSinglePlaneReturnWhite()
    {
        shapes.Add(new Plane());
        World world = new World(shapes, 
            new LightSource(new([10, 1, 0]), 1), backgroundColor);

        Line line = new Line(new([1, 0, 0]), new([-1, 1, 0]));

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Equal(MyColor.White,
            world.calcColorAtIntersectionWrapper(intersection, line));
    }

    [Fact]
    public void getBrightnessWithSingleRasterPlaneNotOnLineReturnWhite()
    {
        shapes.Add(new PlaneRaster());
        World world = new World(shapes,
            new LightSource(new([10, 0.5, 0.5]), 1), backgroundColor);

        Line line = new Line(new([0.5, 0, 0]), new([-1, 1, 1]));

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Equal(MyColor.White,
            world.calcColorAtIntersectionWrapper(intersection, line));
    }

    [Fact]
    public void getBrightnessWithSingleRasterPlaneNotOnReturnBlack()
    {
        shapes.Add(new PlaneRaster());
        World world = new World(shapes,
            new LightSource(new([10, 1, 0]), 1), backgroundColor);

        Line line = new Line(new([1, 0, 0]), new([-1, 1, 0]));

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Equal(MyColor.Black,
            world.calcColorAtIntersectionWrapper(intersection, line));
    }

    [Fact]
    public void getBrightnessWithOnShadowSideShouldReturnBlack()
    {
        shapes.Add(new Sphere());
        World world = new World(shapes, new LightSource(new([-10, 0, 0]), 1), backgroundColor);

        Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

        Intersection intersection = 
            world.getClosestIntersectionWrapper(line);

        Assert.Equal(MyColor.Black,
            world.calcColorAtIntersectionWrapper(intersection, line));
    }

    [Fact]
    public void getBrightnessWithBlockedLightSourceShouldReturnBlack()
    {
        shapes.Add(new Sphere());
        shapes.Add(new Sphere(new([0, 8, 0]), 1, 1, refracts, refractionIndex, color));
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Equal(MyColor.Black,
            world.calcColorAtIntersectionWrapper(intersection, line));
    }

    [Fact]
    public void lightSourceOnOppositeSidePlaneReturnBlack()
    {
        Shape plane = new Plane();
        shapes.Add(plane);
        Line line = new Line(new([1, 0, 0]), new([-1, 0, 0]));

        World world = new World(shapes, new LightSource(new([-1, 0, 0]), 1), backgroundColor);

        Intersection intersection = world.getClosestIntersectionWrapper(line);

        Assert.Equal(MyColor.Black,
            world.calcColorAtIntersectionWrapper(intersection, line));
    }

    //[Fact]
    //public void noShapesReturnBackGroundColor()
    //{
    //    shapes.Add(new Sphere(new([100, 100, 100]),
    //        1, 1, false, 1, MyColor.White));
    //    Line line = new Line(new([1, 0, 0]), new([-1, 0, 0]));

    //    World world = new World(shapes, new LightSource(new([-1, 0, 0]), 1), backgroundColor);

    //    Intersection intersection = 
    //        world.getClosestIntersectionWrapper(line);

    //    Assert.Equal(backgroundColor,
    //        world.calcColorAtIntersectionWrapper(intersection, line));
    //}


    [Fact]
    public void lightSourceInSphereReturnBlack()
    {
        Shape sphere = new Sphere();
        shapes.Add(sphere);
        Line line = new Line(new([2, 0, 0]), new([-1, 0, 0]));

        World world = new World(shapes, 
            new LightSource(new([0, 0, 0]), 1), backgroundColor);

        Intersection intersection = 
            world.getClosestIntersectionWrapper(line);

        Assert.Equal(MyColor.Black,
            world.calcColorAtIntersectionWrapper(
                intersection, line));
    }


    [Fact]
    public void viewPointInSphereReturnBlack()
    {
        Shape sphere = new Sphere();
        shapes.Add(sphere);
        Line line = new Line(new([0, 0, 0]), new([1, 0, 0]));

        World world = new World(shapes,
            new LightSource(new([2, 0, 0]), 1), backgroundColor);

        Intersection intersection =
            world.getClosestIntersectionWrapper(line);

        Assert.Equal(MyColor.Black,
            world.calcColorAtIntersectionWrapper(
                intersection, line));
    }


    [Fact]
    public void viewPointAndLightSourceInSphereReturnWhite()
    {
        Shape sphere = new Sphere();
        shapes.Add(sphere);
        Line line = new Line(new([0, 0, 0]), new([1, 0, 0]));

        World world = new World(shapes,
            new LightSource(new([-0.5, 0, 0]), 1), backgroundColor);

        Intersection intersection =
            world.getClosestIntersectionWrapper(line);

        Assert.Equal(MyColor.White,
            world.calcColorAtIntersectionWrapper(
                intersection, line));
    }
}