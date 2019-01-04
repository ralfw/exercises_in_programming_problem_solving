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
        public void Tokenize_tests(string roman, string[] expected) {
            var result = Tokenize(roman);
            Assert.Equal(expected, result.ToArray());
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
                return roman[i].ToString();
            }
        }
        
        private IEnumerable<int> Map(IEnumerable<string> tokens)
        {
            throw new NotImplementedException();
        }
    }
}