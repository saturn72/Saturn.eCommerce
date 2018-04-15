using Shouldly;
using Xunit;

namespace Ordering.Services.Tests
{
    public class ServiceResponseExtensionsTests
    {
        [Theory]
        [InlineData(ServiceResponseResult.BadOrMissingData, "")]
        [InlineData(ServiceResponseResult.BadOrMissingData, "some-error-message")]
        [InlineData(ServiceResponseResult.Success, "some-error-message")]
        public void ServiceResponseExtensions_IsSuccessful_ReturnsFalse(ServiceResponseResult result, string errorMessage)
        {
            new ServiceResponse<string>
            {
                Result = result,
                ErrorMessage = errorMessage
            }.IsSuccessful().ShouldBeFalse();
        }
    }
}
