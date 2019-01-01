using Xunit;

namespace sorting
{
    public class Approach2_tests
    {
        public class Approach1_tests
        {
            [Fact]
            public void Acceptance_test_case_1()
            {
                var values = new[] {3, 10, 7, -2, 5, 10, 2, 0, 9, 4};

                var result = Sort(values);

                var expected = new[] {-2, 0, 2, 3, 4, 5, 7, 9, 10, 10};
                Assert.Equal(expected, result);
                Assert.NotSame(result, values);
            }


            [Fact]
            public void Acceptance_test_case_2()
            {
                var values = new int[0];
                var result = Sort(values);
                Assert.Empty(result);
            }
        }
    }
}