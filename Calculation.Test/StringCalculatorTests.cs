using System;
using Calculation.CustomExceptions;
using NUnit.Framework;

namespace Calculation.Test
{
    [TestFixture]
    public class StringCalculatorTests
    {

        private StringCalculator CreateCalc()
        {
            return new StringCalculator();
        }

        [Test]
        public void Add_EmptyString_ReturnsDefaultValue()
        {
            StringCalculator stringCalculator = CreateCalc();

            int result = stringCalculator.Add("");

            Assert.AreEqual(0, result);
        }

        [TestCase("1", ExpectedResult = 1)]
        [TestCase("2", ExpectedResult = 2)]
        public int Add_SingleNumber_ReturnsThatNumber(string input)
        {
            StringCalculator stringCalculator = CreateCalc();

            return stringCalculator.Add(input);
        }

        [TestCase("1,2", ExpectedResult = 3)]
        [TestCase("1,3", ExpectedResult = 4)]
        public int Add_TwoNumbers_ReturnsSum(string input)
        {
            StringCalculator stringCalculator = CreateCalc();

            return stringCalculator.Add(input);
        }

        [TestCase("1,2,3", ExpectedResult = 6)]
        [TestCase("1,2,3,4", ExpectedResult = 10)]
        public int Add_SeveralNumbers_ReturnsSum(string input)
        {
            StringCalculator stringCalculator = CreateCalc();

            return stringCalculator.Add(input);
        }

        [TestCase("1\n2,3", ExpectedResult = 6)]
        public int Add_SeveralNumbersWithNewLineSeparator_ReturnsSum(string input)
        {
            StringCalculator stringCalculator = CreateCalc();

            return stringCalculator.Add(input);
        }

        [TestCase("//;\n1;2;3", ExpectedResult = 6)]
        [TestCase("//.\n1.2.3", ExpectedResult = 6)]
        public int Add_CustomDelimiter_ReturnsSum(string input)
        {
            StringCalculator stringCalculator = CreateCalc();

            return stringCalculator.Add(input);
        }

        [TestCase("-1")]
        [TestCase("-2")]
        public void Add_NegativeNumber_ThrowsExceptionWithNegativeNumberInMessage(string input)
        {
            StringCalculator stringCalculator = CreateCalc();

            var e = Assert.Catch<NegativeException>(() => stringCalculator.Add(input));
            StringAssert.Contains($"negatives not allowed: {input}", e.Message);
        }

        [TestCase("-1,2", "-1")]
        [TestCase("-1,-2","-1 -2")]
        [TestCase("-1,2,-3", "-1 -3")]
        public void Add_SeveralNegativeNumbers_ThrowsExceptionWithNegativeNumbersInMessage(string input, string expected)
        {
            StringCalculator stringCalculator = CreateCalc();

            var e = Assert.Catch<NegativeException>(() => stringCalculator.Add(input));
            StringAssert.Contains($"negatives not allowed: {expected}", e.Message);
        }

        [TestCase("1,1")]
        public void GetCalledCount_RaisedNumberOfAdd_ReturnsCount(string input)
        {
            StringCalculator stringCalculator = CreateCalc();
            Assert.AreEqual(0, stringCalculator.GetCalledCount());

            stringCalculator.Add(input);
            Assert.AreEqual(1, stringCalculator.GetCalledCount());

            stringCalculator.Add(input);
            Assert.AreEqual(2, stringCalculator.GetCalledCount());
        }


        [TestCase("")]
        [TestCase("1")]
        [TestCase("1,1")]
        public void AddOccured_Add_EventRaisedWithInputAndResult(string input)
        {
            StringCalculator stringCalculator = CreateCalc();

            string givenInput = null;
            int givenResult = 0;
            stringCalculator.AddOccurred += delegate (string input, int result)
            {
                givenInput = input;
                givenResult = result;
            };

            var result = stringCalculator.Add(input);

            Assert.AreEqual(input, givenInput);
            Assert.AreEqual(result, givenResult);
        }
    }
}
