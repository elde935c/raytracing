
using raytracer.Domain.Exceptions;

namespace raytracer.Domain;

public class Vector
{
    private readonly double[] entries;

    public Vector(double[] entries)
    {
        this.entries = entries;
    }

    public Vector(int n)
    {
        entries = new double[n];
    }

    public Vector(Vector vector)
    {
        entries = new double[vector.size()];
        for (int i = 0; i < vector.size(); i++)
            entries[i] = vector[i];
    }

    public double this[int i]
    {
        get { return entries[i]; }
        set { entries[i] = value; }
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        Vector returnVector = new Vector(v1.size());
        for (int i = 0; i < v1.size(); i++)
            returnVector[i] = v1[i] + v2[i];
        return returnVector;
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
        Vector returnVector = new Vector(v1.size());
        for (int i = 0; i < v1.size(); i++)
            returnVector[i] = v1[i] - v2[i];
        return returnVector;
    }

    public static Vector operator* (double scalar, Vector v)
    {
        double[] scalarTimesVectorEntries = new double[v.size()];
        for (int i = 0; i < v.size(); i++)
            scalarTimesVectorEntries[i] = v[i] * scalar;
        return new Vector(scalarTimesVectorEntries);
    }

    public static Vector operator *(Vector v, double scalar)
    {
        return scalar * v;
    }

    public static Vector operator /(Vector v, double scalar)
    {
        if (Math.Abs(scalar) > 1e-9)
        {
            double[] vDividedByScalar = new double[v.size()];
            for (int i = 0; i < v.size(); i++)
                vDividedByScalar[i] = v[i] / scalar;
            return new Vector(vDividedByScalar);
        }
        else throw new DivisionByZeroException();
    }

    public static double dot(Vector v1, Vector v2)
    {
        double dotProduct = 0;
        for (int i = 0; i < v1.size(); i++)
            dotProduct += v1[i] * v2[i];
        return dotProduct;
    }

    public double norm()
    {
        double vectorNorm = 0;
        foreach (double x in entries)
            vectorNorm += x*x;
        return Math.Sqrt(vectorNorm);
    }

    public int size() { return entries.Length; }

    public string ToString()
    {
        return entries.ToString();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Vector other = (Vector)obj;
        return (other - this).norm() < 1e-9;
    }

    public override int GetHashCode()
    {
        return entries.GetHashCode();
    }
}