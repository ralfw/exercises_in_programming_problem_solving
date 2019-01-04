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
        public void Acceptance_tests(string roman, int expected)
        {
            var result = FromRoman(roman);
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("I", new[] {1})]
        [InlineData("V", new[] {5})]
        [InlineData("IVXLCDM", new[] {1,5,10,50,100,500,1000})]
        public void Parse_tests(string roman, int[] expected)
        {
            var result = Parse(roman);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new[] {10,1}, new[] {10,1})]
        [InlineData(new[] {1,10}, new[] {-1,10})]
        public void AdjustForSubtractionRule_test(int[] values, int[] expected)
        {
            var result = AdjustForSubtractionRule(values);
            Assert.Equal(expected, result);
        }
        
        
        private int FromRoman(string roman) {
            var values = Parse(roman);
            values = AdjustForSubtractionRule(values);
            return values.Sum();
        }

        private IEnumerable<int> Parse(string roman)
        {
            return roman.Select(MapDigit);
            
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
            var value = values.First();
            var nextValue = values.Skip(1).First();
            if (value < nextValue) value = -value;
            return new[] {value, nextValue};
        }
    }
}