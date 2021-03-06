using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace from_roman
{
    public class Approach1_tests
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
        [InlineData("I", 1)]
        [InlineData("V", 5)]
        [InlineData("X", 10)]
        [InlineData("L", 50)]
        [InlineData("C", 100)]
        [InlineData("D", 500)]
        [InlineData("M", 1000)]
        public void Convert_single_digit(string roman, int expectedDecimal)
        {
            var result = FromRoman(roman);
            Assert.Equal(expectedDecimal, result);
        }


        [Theory]
        [InlineData("II", 2)]
        [InlineData("XVI", 16)]
        public void Convert_several_digits_without_subtraction_rule(string roman, int expectedDecimal)
        {
            var result = FromRoman(roman);
            Assert.Equal(expectedDecimal, result);
        }
        
        
        [Theory]
        [InlineData("IV", 4)]
        public void Convert_several_digits_with_subtraction_rule(string roman, int expectedDecimal)
        {
            var result = FromRoman(roman);
            Assert.Equal(expectedDecimal, result);
        }

        
        private int FromRoman(string roman)
        {
            return roman.Select(MapDigit)
                        .Aggregate(new List<int>(), AdjustValuesForSubtractionRule)
                        .Sum();

            
            List<int> AdjustValuesForSubtractionRule(List<int> values, int nextValue) {
                if (values.Count > 0 && nextValue > values.Last())
                    values.Add(values.Last() * -2);
                values.Add(nextValue);

                return values;
            }
            
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
    }
}