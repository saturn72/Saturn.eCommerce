using Shouldly;
using Xunit;

namespace Ordering.Services.Tests
{
    public class ServiceResponseTest
    {
        [Fact]
        public void ServiceResponse_SetResultToNotSetInCtor()
        {
            var sr = new ServiceResponse<string>();
            sr.Result.ShouldBe(ServiceResponseResult.NotSet);
        }
    }
}
