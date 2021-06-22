using Calculation.CustomExceptions;

namespace Calculation
{
    public class MyCalc
    {
        public int SpecialAdd(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new NegativeException("bad calc");
            }

            return x + y;
        }
    }
}
