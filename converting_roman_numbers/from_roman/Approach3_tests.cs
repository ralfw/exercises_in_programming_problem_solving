using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace from_roman
{
    public class Approach3_tests
    {
        [Theory]
        [InlineData("MCMLXXXIV", 1984)]
        [InlineData("MCDXCII", 1492)]
        public void Acceptance_tests(string roman, int expected)
        {
            var result = FromRoman(roman);
            Assert.Equal(expected, result);
        }



        [Theory]
        [InlineData("I", new[]{"I"})]
        [InlineData("XVI", new[]{"X","V","I"})]
        [InlineData("IV", new[]{"IV"})]
        [InlineData("MCDXXIV", new[]{"M", "CD", "X", "X", "IV"})]
        public void Tokenize_tests(string roman, string[] expected) {
            var result = Tokenize(roman);
            Assert.Equal(expected, result.ToArray());
        }


        [Theory]
        [InlineData(new[] {"M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"},
            new[] {1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1})]
        public void Map_tests(string[] tokens, int[] expected)
        {
            var result = Map(tokens);
            Assert.Equal(expected, result);
        }
        
        
        private int FromRoman(string roman) {
            var tokens = Tokenize(roman);
            var values = Map(tokens);
            return values.Sum();
        }

        private IEnumerable<string> Tokenize(string roman)
        {
            var i = 0;
            while (i < roman.Length) {
                var token = GetNext();
                yield return token;
                i += token.Length;
            }

            string GetNext() {
                if (i == roman.Length - 1) return roman[i].ToString();

                if (roman[i] == 'I' && (roman[i + 1] == 'V' || roman[i + 1] == 'X') ||
                    roman[i] == 'X' && (roman[i + 1] == 'L' || roman[i + 1] == 'C') ||
                    roman[i] == 'C' && (roman[i + 1] == 'D' || roman[i + 1] == 'M'))
                    return roman.Substring(i, 2);
                
                return roman[i].ToString();
            }
        }
        
        private readonly Dictionary<string,int> _map = new Dictionary<string, int>() {
            {"M", 1000}, {"CM", 900 },
            {"D", 500}, {"CD", 400 },
            {"C", 100}, {"XC", 90 },
            {"L", 50}, {"XL", 40 },
            {"X", 10}, {"IX", 9 },
            {"V", 5}, {"IV", 4 },
            {"I", 1}
        };
        private IEnumerable<int> Map(IEnumerable<string> tokens) => tokens.Select(t => _map[t]);
    }
}