using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Calculation.CustomExceptions;

namespace Calculation
{
    public class StringCalculator
    {
        private const int DEFAULT_VALUE = 0;
        private int count;
        private readonly HashSet<char> delimiters = new();

        public event Action<string, int> AddOccurred;
        public event Action<string> LogErrorOccurred;

        private ISetting _setting;
        private ILogger _logger;

        public StringCalculator(ISetting setting, ILogger logger)
        {
            InitDefaultDelimiters();
            _setting = setting;
            _logger = logger;
        }

        public int Add(string numbers)
        {
            count++;

            if (!_setting.IsEnabled())
            {
                throw new Exception("String calculator is disabled");
            }

            if (count>_setting.MaxUses())
            {
                throw new Exception("Too many uses");
            }

            if (HasNoNumber(numbers))
            {
                AddOccurred?.Invoke(numbers, DEFAULT_VALUE);
                _logger.Write(DEFAULT_VALUE.ToString());
                return DEFAULT_VALUE;
            }

            var cutNumbers = GetAndCutDelimiterFromString(numbers);
            var formattedNumbers = ConvertStringArrayToIntArray(SplitStringByDelimiters(cutNumbers));
            CheckForNegativeNumbers(formattedNumbers);

            var sum = Sum(formattedNumbers);

            AddOccurred?.Invoke(numbers, sum);
            _logger.Write(sum.ToString());
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
            var sum = 0;

            foreach (var number in numbers) sum += number;

            return sum;
        }

        private string[] SplitStringByDelimiters(string numbers)
        {
            string[] splitNumbers;
            if (HasDelimiter(numbers))
                splitNumbers = numbers.Split(delimiters.ToArray());
            else
                splitNumbers = new[] {numbers};

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
            var negativeNumbers = "";
            foreach (var number in numbers)
                if (number < 0)
                    negativeNumbers += $" {number}";
            if (negativeNumbers.Any()) throw new NegativeException($"negatives not allowed:{negativeNumbers}");
        }
    }
}