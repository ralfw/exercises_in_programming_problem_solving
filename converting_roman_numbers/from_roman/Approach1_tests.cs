using System;
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
        public void Convert_single_digit(string roman, int expectedDecimal)
        {
            var result = FromRoman(roman);
            Assert.Equal(expectedDecimal, result);
        }

        
        private int FromRoman(string roman)
        {
            if (roman == "I") return 1;
            if (roman == "V") return 5;
            throw new InvalidOperationException();
        }
    }
}