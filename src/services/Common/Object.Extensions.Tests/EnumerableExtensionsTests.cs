using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Object.Extensions.Tests
{
    public class EnumerableExtensionsTests
    {
        [Theory]
        [MemberData(nameof(EnumerableExtensions_IsNullOrEmpty_True_Data))]
        public void EnumerableExtensions_IsNullOrEmpty_True(IEnumerable<string> source)
        {
            source.IsNullOrEmpty().ShouldBeTrue();
        }

        [Fact]
        public void EnumerableExtensions_IsNullOrEmpty_False()
        {
            new[]{"a", "b"}.IsNullOrEmpty().ShouldBeFalse();
        }


        public static IEnumerable<object[]> EnumerableExtensions_IsNullOrEmpty_True_Data => new[]
        {
            new object[] {null},
            new object[]{new string[] {}}
        };

    }
}
