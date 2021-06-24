using System;
using System.Linq;
using Calculation.CustomExceptions;

namespace Calculation
{
    public class StringCalculator
    {
        private const int DEFAULT_VALUE = 0;
        private int count = 0;
        private char separator = ',';

        public event Action<string, int> AddOccurred;

        public int Add(string numbers)
        {
            count++;

            if (HasNoNumber(numbers))
            {
                AddOccurred?.Invoke(numbers, DEFAULT_VALUE);
                return DEFAULT_VALUE;
            }

            numbers = GetAndCutDelimiterFromString(numbers);
            var formattedNumbers = ConvertStringToInt(SplitStringByDelimiter(numbers));
            CheckForNegativeNumbers(formattedNumbers);

            var sum = Sum(formattedNumbers);

            AddOccurred?.Invoke(numbers, sum);
            return sum;
        }

        public int GetCalledCount()
        {
            return count;
        }

        private int[] ConvertStringToInt(string[] numbers)
        {
            return Array.ConvertAll(numbers, ParseNumberFromString);
        }

        private int Sum(int[] numbers)
        {
            int sum = 0;

            foreach (var number in numbers)
            {
                sum += number;
            }

            return sum;
        }

        private string[] SplitStringByDelimiter(string numbers)
        {
            string[] splitNumbers;
            if (HasDelimiter(numbers))
            {
                splitNumbers = numbers.Split(separator, '\n');
            }
            else
            {
                splitNumbers = new[] {numbers};
            }

            return splitNumbers;
        }

        private int ParseNumberFromString(string splitNumber)
        {
            return int.Parse(splitNumber);
        }

        private bool HasDelimiter(string numbers)
        {
            return numbers.Contains(separator) || numbers.Contains('\n');
        }

        private bool HasNoNumber(string numbers)
        {
            return numbers.Length == 0;
        }

        private string GetAndCutDelimiterFromString(string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                separator = numbers[2];
                return numbers.Substring(4);
            }

            return numbers;
        }

        private void CheckForNegativeNumbers(int[] numbers)
        {
            string negativeNumbers = "";
            foreach (var number in numbers)
            {
                if (number < 0)
                {
                    negativeNumbers += $" {number}";
                }
            }
            if (negativeNumbers.Any())
            {
                throw new NegativeException($"negatives not allowed:{negativeNumbers}");
            }
        }
    }
}
