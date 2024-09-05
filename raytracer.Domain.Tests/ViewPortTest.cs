using Xunit;
using raytracer.Domain;
using System.Diagnostics;
using System.Drawing;

namespace raytracer.Domain.Tests;

public class ViewPortTest
{
    Color color = Color.White;
    Color backgroundColor = Color.DarkBlue;

    [Fact]
    public void testIfPictureIsMadeUnitSphereLigthFromAbove()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Sphere());
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Vector[] corners = [new([-0.5, 0.5, -4]),
                            new([0.5, 0.5, -4]),
                            new([-0.5, -0.5, -4])];

        Vector viewPoint = new([0, 0, -6]);

        Scene scene = new Scene(world, corners, viewPoint, 720, 720);

        ViewPort viewPort = new ViewPort(corners, viewPoint, 720, 720, scene);

        viewPort.createImage("outputTest1.png", false);
    }



    [Fact]
    public void testIfPictureIsMadeMultipleSphereLigthFromAbove()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Sphere());
        Boolean refracts = false;
        double refractionIndex = 1;
        shapes.Add(new Sphere(new([3, 0, 2]), 1, 1, refracts, refractionIndex, color));
        shapes.Add(new Sphere(new([0, 2, -2]), 0.2, 0.5, refracts, refractionIndex, color));
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Vector[] corners = [new([-0.5, 0.5, -4]),
                        new([0.5, 0.5, -4]),
                        new([-0.5, -0.5, -4])];

        Vector viewPoint = new([0, 0, -5]);

        Scene scene = new Scene(world, corners, viewPoint, 720, 720);

        ViewPort viewPort = new ViewPort(corners, viewPoint, 720, 720, scene);

        viewPort.createImage("outputTest2.png", false);
    }

    [Fact]
    public void testRuntimeParallelVsSerial()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Sphere());
        Boolean refracts = false;
        double refractionIndex = 1;
        shapes.Add(new Sphere(new([3, 0, 2]), 1, 1, refracts, refractionIndex, color));
        shapes.Add(new Sphere(new([0, 2, -2]), 0.2, 0.5, refracts, refractionIndex, color));
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Vector[] corners = [new([-0.5, 0.5, -4]),
                        new([0.5, 0.5, -4]),
                        new([-0.5, -0.5, -4])];

        Vector viewPoint = new([0, 0, -5]);

        int screenWidth = 720;
        int screenHeight = 720;

        Scene scene = new Scene(world, corners, viewPoint,
            screenWidth, screenHeight);

        ViewPort viewPort = new ViewPort(corners, viewPoint,
            screenWidth, screenHeight, scene);

        var watch = System.Diagnostics.Stopwatch.StartNew();
        viewPort.createImage("outputTest2.png", false);
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Debug.WriteLine(elapsedMs);

        watch = System.Diagnostics.Stopwatch.StartNew();
        viewPort.createImage("outputTest2.png", true);
        watch.Stop();
        var elapsedMsParallel = watch.ElapsedMilliseconds;
        Debug.WriteLine(elapsedMsParallel);

        Assert.True(elapsedMsParallel < elapsedMs);
    }

    [Fact]
    public void testPictureWithOneLenseOneSphere()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Sphere());
        shapes.Add(new Sphere(new([3, 0, 0]), 0.1, 1, true, 1.5, color));
        
        World world = new World(shapes,
            new LightSource(new([0, 10, 0]), 1), backgroundColor);

        Vector[] corners = [new([6, 0.5, -0.5]),
                        new([6, 0.5, 0.5]),
                        new([6, -0.5, -0.5])];

        Vector viewPoint = new([8, 0, 0]);

        Scene scene = new Scene(world, corners, viewPoint, 720, 720);

        ViewPort viewPort = new ViewPort(corners, viewPoint, 720, 720, scene);

        viewPort.createImage("outputTestLense.png", true);
    }

    [Fact]
    public void viewFromWithinSphere()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Sphere(new([0, 0, 0]), 20, 1, false, 1.5, color));

        World world = new World(shapes,
            new LightSource(new([0, 19, 0]), 1), backgroundColor);

        Vector[] corners = [new([18, 0.5, -0.5]),
                        new([18, 0.5, 0.5]),
                        new([18, -0.5, -0.5])];

        Vector viewPoint = new([19, 0, 0]);

        Scene scene = new Scene(world, corners, viewPoint, 720, 720);

        ViewPort viewPort = new ViewPort(corners, viewPoint, 720, 720, scene);

        viewPort.createImage("withinSphereView.png", true);
    }
}

