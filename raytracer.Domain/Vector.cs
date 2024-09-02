
namespace raytracer.Domain;

public class Vector
{
    private double[] entries;

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
            entries[i] = vector.get(i);
    }

    public double get(int index) { return entries[index]; }

    public void add(int index, double value) { entries[index] += value; }

    public static Vector operator +(Vector v1, Vector v2)
    {
        Vector returnVector = v1;
        for (int i = 0; i < v1.size(); i++)
            returnVector.add(i, v2.get(i));
        return returnVector;
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
        Vector returnVector = v1;
        for (int i = 0; i < v1.size(); i++)
            returnVector.add(i, -v2.get(i));
        return returnVector;
    }

    public static Vector operator* (double scalar, Vector v)
    {
        double[] scalarTimesVectorEntries = new double[v.size()];
        for (int i = 0; i < v.size(); i++)
            scalarTimesVectorEntries[i] = v.get(i) * scalar;
        return new Vector(scalarTimesVectorEntries);
    }

    public static Vector operator /(Vector v, double scalar)
    {
        double[] vDividedByScalar = new double[v.size()];
        for (int i = 0; i < v.size(); i++)
            vDividedByScalar[i] = v.get(i) / scalar;
        return new Vector(vDividedByScalar);
    }

    public static double dot(Vector v1, Vector v2)
    {
        double dotProduct = 0;
        for (int i = 0; i < v1.size(); i++)
            dotProduct += v1.get(i) * v2.get(i);
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