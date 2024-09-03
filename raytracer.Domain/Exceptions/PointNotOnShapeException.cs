namespace raytracer.Domain.Exceptions
{
    public class PointNotOnShapeException : Exception
    {
        public PointNotOnShapeException(string message)
        {
            new Exception(message);
        }
    }
}
