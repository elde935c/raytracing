using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
