using Shouldly;
using Xunit;

namespace Ordering.Services.Tests
{
    public class ServiceResponseExtensionsTests
    {
        [Theory]
        [InlineData(ServiceResponseResult.BadOrMissingData, "")]
        [InlineData(ServiceResponseResult.InternalError, "some-error-message")]
        public void ServiceResponseExtensions_IsSuccessful_ReturnsFalse(ServiceResponseResult result, string errorMessage)
        {
            new ServiceResponse<string>
            {
                Result = result,
                ErrorMessage = errorMessage
            }.IsSuccessful().ShouldBeFalse();
        }

        [Theory]
        [InlineData(ServiceResponseResult.Success)]
        [InlineData(ServiceResponseResult.Created)]
        public void ServiceResponseExtensions_IsSuccessful_ReturnsTrue(ServiceResponseResult result)
        {
            new ServiceResponse<string>
            {
                Result = result,
            }.IsSuccessful().ShouldBeTrue();
        }
    }
}
