namespace raytracer.Domain.Exceptions
{
    public class DivisionByZeroException : Exception
    {
        public DivisionByZeroException() 
        {
            new Exception("you can not divide by 0");
        }
    }
}
