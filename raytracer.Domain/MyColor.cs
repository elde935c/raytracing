//using System.Drawing;

namespace raytracer.Domain;

public class MyColor 
{// had to define a custom color class to override the Equals operator
    public int R { get; private set; }
    public int G { get; private set; }
    public int B { get; private set; }


    public MyColor(int r, int g, int b)
    {
        R=r; 
        G=g; 
        B=b;
    }

    public MyColor(MyColor color)
    {
        this.R = color.R;
        this.G = color.G;
        this.B = color.B;
    }

    public static MyColor operator *(double scalar, MyColor c)
    {
        int r = (int)(c.R * scalar);
        int g = (int)(c.G * scalar);
        int b = (int)(c.B * scalar);
        return new MyColor(r,g,b);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        MyColor other = (MyColor)obj;
        return (other.R == this.R) && 
            (other.B == this.B) &&
            (other.G == this.G);
    }

    public override int GetHashCode()
    {
        return R.GetHashCode() + G.GetHashCode() + B.GetHashCode();
    }


    public static MyColor White { get; } = new MyColor(255, 255, 255);
    public static MyColor Black { get; } = new MyColor(0, 0, 0);
    public static MyColor DarkBlue { get; } = new MyColor(0, 0, 55);
    public static MyColor Red { get; } = new MyColor(255, 0, 0);
    public static MyColor Green { get; } = new MyColor(0, 255, 0);
    public static MyColor Blue { get; } = new MyColor(0, 0, 255);
}