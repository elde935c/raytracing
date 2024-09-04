using Xunit;
using raytracer.Domain;


namespace raytracer.Domain.Tests
{
    public class WorldTest
    {
        Boolean refracts = false;
        double refractionIndex = 1;
        List<Shape> shapes;

        public WorldTest() 
        {
            shapes = new List<Shape>();
        }


        [Fact]
        public void testIfClosestIntersectionIsReturnedOneSphere()
        {
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
            shapes.Add(new Sphere());
            shapes.Add(new Sphere(new([-3, 0, 0]), 1, 1, refracts, refractionIndex));
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
            shapes.Add(new Sphere());
            shapes.Add(new Sphere(new([-3, 0, 0]), 3, 1, refracts, refractionIndex));
            World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

            Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

            Vector solution = new([1, 0, 0]);

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Equal(solution, intersection.getCoord());
        }

        [Fact]
        public void testLineNotIntersectionShouldReturnNull()
        {
            shapes.Add(new Sphere());
            shapes.Add(new Sphere(new([-3, 0, 0]), 1, 1, refracts, refractionIndex));
            World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

            Line line = new Line(new([-1.5, -10, 0]), new([0, 1, 0]));

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Null(intersection);
        }

        [Fact]
        public void lineThroughLenseToNothing()
        {
            shapes.Add(new Sphere(new([-3, 0, 0]), 1, 1,
                true, refractionIndex));
            World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

            Line line = new Line(new([-10, 0, 0]), new([1, 0, 0]));

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Null(intersection);

        }

        [Fact]
        public void lineStraightThroughPlaneToSphere()
        {
            Sphere sphere = new Sphere();
            shapes.Add(sphere);
            shapes.Add(new Plane(new([-3,0,0]),
                new([1,0,0]), 1, true, 1));
            World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

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
                true, refractionIndex));
            shapes.Add(new Sphere(new([-5, 0, 0]), 1, 1,
                true, refractionIndex));
            World world = new World(shapes, 
                new LightSource(new([0, 10, 0]), 1));

            Line line = new Line(new([-10, 0, 0]), new([1, 0, 0]));

            Intersection intersection =
                world.getClosestIntersectionWrapper(line);
            Intersection solution = new Intersection(new([-1, 0, 0]),
                sphere);

            Assert.Equal(solution, intersection);

        }

        [Fact]
        public void returnNullWhenTrappedInSphere()
        {
            Vector center = new([0, 0, 0]);
            Sphere smallSphere = new Sphere(center, 1, 1, true,
                100);
            Sphere largeSphere = new Sphere(center, 2, 1, false,
                refractionIndex);
            shapes.Add(smallSphere);
            shapes.Add(largeSphere);
            World world = new World(shapes,
                new LightSource(new([0, 10, 0]), 1));
            Line line = new Line(new([0.99, 0, 0]), new([0, 1, 0]));

            Intersection intersection =
                world.getClosestIntersectionWrapper(line);

            //Assert.Null(intersection);
            Assert.Fail();
        }

        [Fact]
        public void getBrightnessWithSingleSphere()
        {
            shapes.Add(new Sphere());
            World world = new World(shapes, new LightSource(new([10, 0, 0]), 1));

            Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Equal(1, world.calcBrightnessWrapper(intersection));
        }

        [Fact]
        public void getBrightnessWithOnShadowSideShouldReturnZero()
        {
            shapes.Add(new Sphere());
            World world = new World(shapes, new LightSource(new([-10, 0, 0]), 1));

            Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Equal(0, world.calcBrightnessWrapper(intersection));
        }

        [Fact]
        public void getBrightnessWithBlockedLightSourceShouldReturnZero()
        {
            shapes.Add(new Sphere());
            shapes.Add(new Sphere(new([0, 8, 0]), 1, 1, refracts, refractionIndex));
            World world = new World(shapes, new LightSource(new([0, 10, 0]), 1));

            Line line = new Line(new([10, 0, 0]), new([-1, 0, 0]));

            Intersection intersection = world.getClosestIntersectionWrapper(line);

            Assert.Equal(0, world.calcBrightnessWrapper(intersection));
        }
    }
}