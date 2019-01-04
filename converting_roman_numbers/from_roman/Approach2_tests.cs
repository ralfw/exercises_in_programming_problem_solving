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


        
        private int FromRoman(string roman)
        {
            throw new NotImplementedException();
        }
    }
}