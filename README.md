# raytracer

Raytracer with objects that can mirror and refract. The air refraction index is assumed to be 1. Giving an object an index other than 1 makes it refract the light rays. If the index \ll 1 it becomes a mirror

This project was created to teach myself how to make unit tests in Csharp

There is no user interface. Test case scenarios can be run in `raytracer.Domain.Tests/ViewPortTest.cs` 

### Example
```cs
// create a list of shapes for the light to shine on/refract with
// possible shapes are sphere and plane
List<Shape> shapes = new List<Shape>();
shapes.Add(new Sphere());

// add the shapes to the world, including a lightsource with a coordinate and intensity
World world = new World(shapes, new LightSource(new([0, 10, 0]), 1), backgroundColor);

// the view is from the viewPoint through the rectangle defined by the 3 coordinates in corners
Vector[] corners = [new([-0.5, 0.5, -4]),
                    new([0.5, 0.5, -4]),
                    new([-0.5, -0.5, -4])];
Vector viewPoint = new([0, 0, -6]);

// combine the figures, lightsource, viewpoint, corners and the horizontal and vertical resolution
Scene scene = new Scene(world, corners, viewPoint, 720, 720);

// class to make the images
ViewPort viewPort = new ViewPort(corners, viewPoint, 720, 720, scene);

viewPort.createImage("outputTest1.png", false);

```