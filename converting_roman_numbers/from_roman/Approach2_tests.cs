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
        
        
        private int FromRoman(string roman) {
            var values = Parse(roman);
            values = AdjustForSubtractionRule(values);
            return values.Sum();
        }

        private int[] Parse(string roman)
        {
            return roman.Select(MapDigit).ToArray();
            
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

        private int[] AdjustForSubtractionRule(int[] values)
        {
            var adjuestedValues = new List<int>();
            
            var value = values.First();
            foreach (var nextValue in values.Skip(1)) {
                if (value < nextValue) value = -value;
                adjuestedValues.Add(value);

                value = nextValue;
            }
            adjuestedValues.Add(value);

            return adjuestedValues.ToArray();
        }
    }
}