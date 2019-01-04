using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace from_roman
{
    public class Approach2_tests
    {
        [Theory]
        [InlineData("MCMLXXXIV", 1984)]
        [InlineData("MCDXCII", 1492)]
        public void Acceptance_tests(string roman, int expectedDecimal)
        {
            var result = FromRoman(roman);
            Assert.Equal(expectedDecimal, result);
        }
        
        [Theory]
        [InlineData("I", new[] {1})]
        public void Parse_tests(string roman, int[] expectedValues)
        {
            var result = Parse(roman);
            Assert.Equal(expectedValues, result);
        }
        
        
        private int FromRoman(string roman) {
            var values = Parse(roman);
            values = AdjustForSubtractionRule(values);
            return values.Sum();
        }

        private IEnumerable<int> Parse(string roman)
        {
            return new[] {1};
        }

        private IEnumerable<int> AdjustForSubtractionRule(IEnumerable<int> values)
        {
            throw new NotImplementedException();
        }
    }
}