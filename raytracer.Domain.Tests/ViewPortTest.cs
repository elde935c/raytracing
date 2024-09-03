using Xunit;
using raytracer.Domain;

namespace raytracer.Domain.Tests;

public class ViewPortTest
{
    [Fact]
    public void testIfPictureIsMade()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Sphere());
        shapes.Add(new Sphere(new([0, 8, 0]), 1, 1));
        World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

        Vector[] corners = [new([-0.5, 0.5, -4]),
                            new([0.5, 0.5, -4]),
                            new([-0.5, -0.5, -4])];

        Vector viewPoint = new([0, 0, -8]);

        Scene scene = new Scene(world, corners, viewPoint, 10, 10);

        ViewPort viewPort = new ViewPort(corners, viewPoint, 10, 10, scene);

        viewPort.createImage();
    }
}
