using Xunit;
using raytracer.Domain;
using raytracer.Domain.Exceptions;

namespace raytracer.Domain.Tests;
public class LineTest
{
    [Fact]
    public void testIfTwoIdenticalLinesAreTheEqual()
    {
        Line line1 = new Line(new([0,0,0]), new([0,1,0]));
        Line line2 = new Line(new([0, 0, 0]), new([0, 1, 0]));

        Assert.Equal(line1, line2);
    }

    [Fact]
    public void testIfTwoLinesWithDifferentStartAreNotEqual()
    {
        Line line1 = new Line(new([0, 0, 0]), new([0, 1, 0]));
        Line line2 = new Line(new([0, 0, 1]), new([0, 1, 0]));

        Assert.NotEqual(line1, line2);
    }

    [Fact]
    public void testIfTwoLinesWithDifferentDirectionAreNotEqual()
    {
        Line line1 = new Line(new([0, 0, 0]), new([0, 1, 0]));
        Line line2 = new Line(new([0, 0, 0]), new([1, 1, 0]));

        Assert.NotEqual(line1, line2);
    }
}

