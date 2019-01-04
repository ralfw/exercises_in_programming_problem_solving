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
        [InlineData("V", new[] {5})]
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
            return new[] {MapDigit(roman[0])};
            
            int MapDigit(char digit) {
                switch (digit) {
                    case 'I': return 1;
                    case 'V' : return 5;
                    case 'X': return 10;
                    case 'L': return 50;
                    case 'C': return 100;
                    case 'D': return 500;
                    case 'M': return 1000;
                    default: throw new InvalidOperationException();
                }
            }
        }

        private IEnumerable<int> AdjustForSubtractionRule(IEnumerable<int> values)
        {
            throw new NotImplementedException();
        }
    }
}