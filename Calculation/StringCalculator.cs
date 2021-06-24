using System;
using System.Collections.Generic;
using System.Linq;
using Calculation.CustomExceptions;

namespace Calculation
{
    public class StringCalculator
    {
        private const int DEFAULT_VALUE = 0;
        private int count = 0;
        private HashSet<char> delimiters = new HashSet<char>();

        public event Action<string, int> AddOccurred;

        public int Add(string numbers)
        {
            count++;
            InitDefaultDelimiters();

            if (HasNoNumber(numbers))
            {
                AddOccurred?.Invoke(numbers, DEFAULT_VALUE);
                return DEFAULT_VALUE;
            }

            numbers = GetAndCutDelimiterFromString(numbers);
            var formattedNumbers = ConvertStringArrayToIntArray(SplitStringByDelimiters(numbers));
            CheckForNegativeNumbers(formattedNumbers);

            var sum = Sum(formattedNumbers);

            AddOccurred?.Invoke(numbers, sum);
            return sum;
        }

        private void InitDefaultDelimiters()
        {
            delimiters.Add(',');
            delimiters.Add(';');
            delimiters.Add('\n');
        }

        public int GetCalledCount()
        {
            return count;
        }

        private int[] ConvertStringArrayToIntArray(string[] numbers)
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

        private string[] SplitStringByDelimiters(string numbers)
        {
            string[] splitNumbers;
            if (HasDelimiter(numbers))
            {
                splitNumbers = numbers.Split(delimiters.ToArray());
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
            return delimiters.Any(numbers.Contains);
        }

        private bool HasNoNumber(string numbers)
        {
            return numbers.Length == 0;
        }

        private string GetAndCutDelimiterFromString(string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                delimiters.Add(numbers[2]);
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
