using Xunit;
using raytracer.Domain;

namespace raytracer.Domain.Tests;

public class ViewPortTest
{
    [Fact]
    public void testIfPictureIsMadeUnitSphereLigthFromAbove()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Sphere());
        //shapes.Add(new Sphere(new([3, 0, 2]), 1, 1));
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

        Vector[] corners = [new([-0.5, 0.5, -4]),
                            new([0.5, 0.5, -4]),
                            new([-0.5, -0.5, -4])];

        Vector viewPoint = new([0, 0, -6]);

        Scene scene = new Scene(world, corners, viewPoint, 720, 720);

        ViewPort viewPort = new ViewPort(corners, viewPoint, 720, 720, scene);

        viewPort.createImage("outputTest1.png");
    }



    [Fact]
    public void testIfPictureIsMadeMultipleSphereLigthFromAbove()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Sphere());
        shapes.Add(new Sphere(new([3, 0, 2]), 1, 1));
        shapes.Add(new Sphere(new([0, 2, -2]), 0.2, 0.5));
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

        Vector[] corners = [new([-0.5, 0.5, -4]),
                        new([0.5, 0.5, -4]),
                        new([-0.5, -0.5, -4])];

        Vector viewPoint = new([0, 0, -5]);

        Scene scene = new Scene(world, corners, viewPoint, 720, 720);

        ViewPort viewPort = new ViewPort(corners, viewPoint, 720, 720, scene);

        viewPort.createImage("outputTest2.png");
    }
}

