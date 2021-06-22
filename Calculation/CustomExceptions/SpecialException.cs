using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation.CustomExceptions
{
    public class SpecialException : Exception
    {
        public SpecialException(string message) : base(message)
        {

        }
    }
}
