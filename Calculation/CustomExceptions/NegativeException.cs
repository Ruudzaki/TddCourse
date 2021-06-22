using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation.CustomExceptions
{
    public class NegativeException : SpecialException
    {
        public NegativeException(string message) : base(message)
        {

        }
    }
}
