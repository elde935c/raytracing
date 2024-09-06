using Xunit;
using raytracer.Domain;
using System.Diagnostics;
using System.Drawing;

namespace raytracer.Domain.Tests;



public class ViewPortTest
{ // this class contains methods to create pictures
  // these are not unit tests, so they are disabled
    MyColor color = MyColor.White;
    MyColor backgroundColor = MyColor.DarkBlue;
    const string skip = "Class ViewPortTest disabled";

    [Fact(Skip = skip)]
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



    [Fact(Skip = skip)]
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

    [Fact(Skip = skip)]
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

    [Fact(Skip = skip)]
    public void testPictureWithOneLenseOneSphere()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Sphere());
        shapes.Add(new Sphere(new([3, 0, 0]), 0.3, 1, true, 1.5, color));
        
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

    [Fact(Skip = skip)]
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

    [Fact(Skip = skip)]
    public void viewOfRasterGrid() { 
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new PlaneRaster());

        World world = new World(shapes,
            new LightSource(new([10, 0, 0]), 1), backgroundColor);

        Vector[] corners = [new([5, 0.5, -0.5]),
                        new([5, 0.5, 0.5]),
                        new([5, -0.5, -0.5])];

        Vector viewPoint = new([7, 0, 0]);

        Scene scene = new Scene(world, corners, viewPoint, 720, 720);

        ViewPort viewPort = 
            new ViewPort(corners, viewPoint, 720, 720, scene);

        viewPort.createImage("planeRasterView.png", true);
    }

    [Fact(Skip = skip)]
    public void viewOfRasterGridThroughLenseSameRefractionIndexShouldNotDistort()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new PlaneRaster());
        shapes.Add(new Sphere(new([10, 0, 0]), 1, 1, true, 1, color));

        double xCoord = 40;

        World world = new World(shapes,
            new LightSource(new([xCoord, 0, 0]), 1), backgroundColor);

        Vector[] corners = [new([xCoord-2, 0.5, -0.5]),
                        new([xCoord - 2, 0.5, 0.5]),
                        new([xCoord - 2, -0.5, -0.5])];

        Vector viewPoint = new([xCoord, 0, 0]);

        Scene scene = new Scene(world, corners, viewPoint, 720, 720);

        ViewPort viewPort =
            new ViewPort(corners, viewPoint, 720, 720, scene);

        viewPort.createImage("planeRasterViewThroughLense.png", true);
    }


    [Fact(Skip = skip)]
    public void viewOfRasterGridThroughLenseHighRefractionIndexShouldDistort()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new PlaneRaster());
        shapes.Add(new Sphere(new([10, 0, 0]), 5, 1, true, 10, color));

        double xCoord = 40;

        World world = new World(shapes,
            new LightSource(new([xCoord, 0, 0]), 1), backgroundColor);

        Vector[] corners = [new([xCoord-1, 0.5, -0.5]),
                        new([xCoord - 1, 0.5, 0.5]),
                        new([xCoord - 1, -0.5, -0.5])];

        Vector viewPoint = new([xCoord, 0, 0]);

        int numPixels = 1080;

        Scene scene = new Scene(world, corners, viewPoint,
            numPixels, numPixels);

        ViewPort viewPort =
            new ViewPort(corners, viewPoint, numPixels,
            numPixels, scene);

        viewPort.createImage("planeRasterViewThroughLense.png", true);
    }


    [Fact(Skip = skip)]
    public void RasterGridBehindSphere()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new PlaneRaster(new([0,-2,-2]), new([1,0,0]),
        1, false, 1, MyColor.White, new([0,0,1]), MyColor.Green,
        0.2, 5));
        shapes.Add(new Sphere(new([10, 0, 0]), 5, 1, true, 1.1, color));

        double xCoord = 40;

        World world = new World(shapes,
            new LightSource(new([xCoord, 0, 0]), 1), backgroundColor);

        Vector[] corners = [new([xCoord-1, 0.5, -0.5]),
                        new([xCoord - 1, 0.5, 0.5]),
                        new([xCoord - 1, -0.5, -0.5])];

        Vector viewPoint = new([xCoord, 0, 0]);

        int numPixels = 1080;

        Scene scene = new Scene(world, corners, viewPoint,
            numPixels, numPixels);

        ViewPort viewPort =
            new ViewPort(corners, viewPoint, numPixels,
            numPixels, scene);

        viewPort.createImage("planeRasterBehindSphere.png", true);
    }

    [Fact(Skip = skip)]
    public void sphereAsMirrorNextToRaster()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new PlaneRaster());

        double xCoord = 10;
        double yCoord = 5;
        double screenWidth = 0.5;

        shapes.Add(new Sphere(new([xCoord, 0, 0]), 2, 1, true,
            0.1, color));
               

        World world = new World(shapes,
            new LightSource(new([xCoord, yCoord, 0]), 1), backgroundColor);

        Vector[] corners = [new([xCoord-0.5, yCoord, 0.5]),
                        new([xCoord+0.5, yCoord, 0.5]),
                        new([xCoord-0.5, yCoord, -0.5])];

        Vector viewPoint = new([xCoord, yCoord + 1, 0]);

        int numPixels = 720;

        Scene scene = new Scene(world, corners, viewPoint,
            numPixels, numPixels);

        ViewPort viewPort =
            new ViewPort(corners, viewPoint, numPixels,
            numPixels, scene);

        viewPort.createImage("mirrorSphereWithRaster.png", true);
    }
}

