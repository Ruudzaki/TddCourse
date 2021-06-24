using Calculation.CustomExceptions;
using NUnit.Framework;

namespace Calculation.Test
{
    [TestFixture]
    public class CalcTests
    { 
        private MyCalc CreateCalc()
        {
            return new MyCalc();
        }

        [TestCase(1, 2, 3)]
        [TestCase(1, 3, 4)]
        public void SpecialAddTwoPositiveNumbersSum(int x, int y, int expected)
        {
            var myCalc = CreateCalc();

            int res = myCalc.SpecialAdd(x, y);

            Assert.AreEqual(expected, res);
        }

        [TestCase(-1, 2, "bad calc")]
        [TestCase(1, -2, "bad calc")]
        [TestCase(-1, -2, "bad calc")]
        public void SpecialAddWithNegativeNumberThrowsException(int x, int y, string message)
        {
            var myCalc = CreateCalc();

            var e = Assert.Catch<SpecialException>(() => myCalc.SpecialAdd(x, y));
            StringAssert.Contains(message, e.Message);
        }
    }
}
