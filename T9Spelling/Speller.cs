using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T9Spelling
{
    public class Speller
    {
        private const string Pause = " ";
        private static readonly Dictionary<char, string> Mapping = new Dictionary<char, string>
        {
            {'a', "2"},
            {'b', "22"},
            {'c', "222"},
            {'d', "3"},
            {'e', "33"},
            {'f', "333"},
            {'g', "4"},
            {'h', "44"},
            {'i', "444"},
            {'j', "5"},
            {'k', "55"},
            {'l', "555"},
            {'m', "6"},
            {'n', "66"},
            {'o', "666"},
            {'p', "7"},
            {'q', "77"},
            {'r', "777"},
            {'s', "7777"},
            {'t', "8"},
            {'u', "88"},
            {'v', "888"},
            {'w', "9"},
            {'x', "99"},
            {'y', "999"},
            {'z', "9999"},
            {' ', "0"}
        };

        private readonly StringBuilder _output = new StringBuilder();
        private char _lastChar;

        private void Append(string str)
        {
            if (_lastChar == str[0])
                _output.Append(Pause);
            _output.Append(str);
            _lastChar = str.Last();
        }

        public string Spell(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException(nameof(input));

            // init
            _output.Clear();
            _lastChar = '-';

            for (var i = 0; i < input.Length; i++)
            {
                if (!Mapping.TryGetValue(input[i], out var str))
                    throw new InvalidCharException(input[i], i + 1);
                Append(str);
            }

            return _output.ToString();
        }

        public string[] Spell(string[] input)
        {
            if (input == null || input.Length == 0)
                throw new ArgumentException(nameof(input));

            var result = new string[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                try
                {
                    result[i] = Spell(input[i]);
                }
                catch (Exception ex)
                {
                    throw new InvalidCaseException(i+1, ex);
                }
            }

            return result;
        }
    }

    public class InvalidCharException: Exception
    {
        public InvalidCharException(char c, int position)
            : base($"Invalid char '{c}' in position {position}")
        {
        }
    }

    public class InvalidCaseException: Exception
    {
        public InvalidCaseException(int caseNumber, Exception innerException)
            : base($"Invalid case #{caseNumber}: {innerException.Message}", innerException)
        {
        }
    }
}