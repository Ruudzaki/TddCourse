using Calculation.CustomExceptions;
using NUnit.Framework;

namespace Calculation.Test
{
    public class CalcTests
    {
        [TestCase(1,2, 3)]
        [TestCase(1,3, 4)]
        public void SpecialAddTwoPositiveNumbersSum(int x, int y, int expected)
        {
            MyCalc c = new MyCalc();

            int res = c.SpecialAdd(x, y);

            Assert.AreEqual(expected, res);
        }

        [TestCase(-1, 2)]
        [TestCase(1, -2)]
        [TestCase(-1, -2)]
        public void SpecialAddWithNegativeNumberThrowsException(int x, int y)
        {
            MyCalc c = new MyCalc();
            
            var e = Assert.Catch<SpecialException>(() => c.SpecialAdd(x, y));
            StringAssert.Contains("bad calc", e.Message);
            
        }
    }
}
