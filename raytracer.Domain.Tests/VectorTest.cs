namespace raytracer.Domain.Tests;
using Xunit;
using raytracer.Domain;
using raytracer.Domain.Exceptions;

public class VectorTest
{
    [Fact]
    public void testIfSameVectorsAreEqual()
    {
        Vector v1 = new Vector([1, 2, 3]);
        Vector v2 = new Vector([1,2,3]);

        Assert.Equal(v1, v2);
    }

    [Fact]
    public void testIfDifferentVectorsAreNotEqual()
    {
        Vector v1 = new Vector([1, 2, 3]);
        Vector v2 = new Vector([5, 3, 6]);

        Assert.NotEqual(v1, v2);
    }

    [Fact]
    public void testCopyConstructor()
    {
        Vector v1 = new Vector(new double[] { 1, 2, 3 });
        Vector v2 = v1;

        Assert.Equal(v1, v2);
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(1, 3)]
    [InlineData(2, 6)]
    public void testBrackerOperator(int index, double value)
    {
        Vector v = new([2, 3, 6]);
        Assert.Equal(value, v[index]);
    }


    [Fact]
    public void checkAddition()
    {
        Vector v1 = new Vector(new double[] { 1, 2, 3 });
        Vector v2 = new Vector(new double[] { 2,5,1});
        Vector solution = new Vector(new double[] { 3, 7, 4 });

        Assert.Equal(solution, v1 + v2);
    }

    [Fact]
    public void checkSubtraction()
    {
        Vector v1 = new Vector(new double[] { 1, 2, 3 });
        Vector v2 = new Vector(new double[] { 2, 5, 1 });
        Vector solution = new Vector(new double[] { -1, -3, 2 });

        Assert.Equal(solution, v1 - v2);
    }

    [Fact]
    public void checkNorm()
    {
        Vector v1 = new([1, 2, 3]);
        double solution = Math.Sqrt(14);

        Assert.Equal(solution, v1.norm());
    }

    [Fact]
    public void checkMultiplication()
    {
        Vector v1 = new Vector(new double[] { 1, 2, 3 });
        double scalar = 3;
        Vector solution = new([3, 6, 9]);

        Assert.Equal(solution, scalar*v1);
    }

    [Fact]
    public void checkDivision()
    {
        Vector v1 = new Vector(new double[] { 1, 2, 3 });
        double scalar = 3;
        Vector solution = new([1/scalar, 2 / scalar, 3 / scalar]);

        Assert.Equal(solution, v1/scalar);
    }

    [Fact]
    public void checkDivisionByZero()
    {
        Vector v1 = new Vector(new double[] { 1, 2, 3 });
        double scalar = 0;

        Assert.Throws<DivisionByZeroException>(() => 
            v1/scalar);
    }

    [Fact]
    public void testDotProduct()
    {
        Vector v1 = new Vector(new double[] { 1, 2, 3 });
        Vector v2 = new Vector(new double[] {3, 4,7});

        double dotV1V2 = 32;
        Assert.Equal(dotV1V2, Vector.dot(v1, v2));
    }
}