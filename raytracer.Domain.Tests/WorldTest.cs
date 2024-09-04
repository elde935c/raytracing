using Xunit;
using raytracer.Domain;


namespace raytracer.Domain.Tests
{
    public class WorldTest
    {

        [Fact]
        public void testIfClosestIntersectionIsReturnedOneSphere()
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Sphere());
            World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

            Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

            Vector solution = new([1, 0, 0]);

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Equal(solution, intersection.getCoord());
        }

        [Fact]
        public void testIfClosestIntersectionIsReturnedTwoSpheres()
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Sphere());
            shapes.Add(new Sphere(new([-3, 0, 0]), 1, 1));
            World world = new World(shapes,
                new LightSource(new([0, 10, 0]), 1));

            Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

            Vector solution = new([1, 0, 0]);

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Equal(solution, intersection.getCoord());
        }

        [Fact]
        public void testIfClosestIntersectionIsReturnedOverlappingSpheres()
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Sphere());
            shapes.Add(new Sphere(new([-3, 0, 0]), 3, 1));
            World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

            Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

            Vector solution = new([1, 0, 0]);

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Equal(solution, intersection.getCoord());
        }

        [Fact]
        public void testLineNotIntersectionShouldReturnNull()
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Sphere());
            shapes.Add(new Sphere(new([-3, 0, 0]), 1, 1));
            World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

            Line line = new Line(new([-1.5, -10, 0]), new([0, 1, 0]));

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Null(intersection);
        }

        [Fact]
        public void getBrightnessWithSingleSphere()
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Sphere());
            World world = new World(shapes, new LightSource(new([10, 0, 0]), 1));

            Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

            //Type type = typeof(World);
            //var world = Activator.CreateInstance(type);

            //MethodInfo method = type.GetMethod("calcBrightness",
            //    BindingFlags.NonPublic | BindingFlags.Instance);

            //object[] parameters =
            //{
            //    new Intersection
            //    {
            //        shape = new Sphere(),

            //    }
            //};

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Equal(1, world.calcBrightnessWrapper(intersection));
        }

        [Fact]
        public void getBrightnessWithOnShadowSideShouldReturnZero()
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Sphere());
            World world = new World(shapes, new LightSource(new([-10, 0, 0]), 1));

            Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Equal(0, world.calcBrightnessWrapper(intersection));
        }

        [Fact]
        public void getBrightnessWithBlockedLightSourceShouldReturnZero()
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Sphere());
            shapes.Add(new Sphere(new([0, 8, 0]), 1, 1));
            World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

            Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Equal(0, world.calcBrightnessWrapper(intersection));
        }
    }
}