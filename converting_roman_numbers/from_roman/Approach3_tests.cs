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
        
        
        private int FromRoman(string roman) {
            var tokens = Tokenize(roman);
            var values = Map(tokens);
            return values.Sum();
        }

        private IEnumerable<string> Tokenize(string roman)
        {
            throw new NotImplementedException();
        }
        
        private IEnumerable<int> Map(IEnumerable<string> tokens)
        {
            throw new NotImplementedException();
        }
    }
}