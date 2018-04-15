using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ordering.Services;
using Shouldly;
using Xunit;

namespace Order.WebApi.Tests
{
    public class ServiceResponserExtensionsTests
    {
        [Fact]
        public void ServiceResponseExtensions_ToActionResult_ReturnOk()
        {
            new ServiceResponse<string>
            {
                Result = ServiceResponseResult.Success,
            }.ToActionResult().ShouldBeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData(ServiceResponseResult.BadOrMissingData, null, typeof(BadRequestObjectResult))]
        [InlineData(ServiceResponseResult.Success, "some-error-message", typeof(OkObjectResult))]
        public void ServiceResponseExtensions_ToActionResult_Generic(ServiceResponseResult result, string errorMsg, Type  expObjectResult)
        {
            var srvRes = new ServiceResponse<string>
            {
                Result = result,
                ErrorMessage = errorMsg
            };

            var ar = srvRes.ToActionResult();
            
            ar.ShouldBeOfType(expObjectResult);
        }

        [Theory]
        [InlineData(ServiceResponseResult.Success, "some-error-message", (int) HttpStatusCode.NotAcceptable)]
        public void ServiceResponseExtensions_ToActionResult_StatusCode(ServiceResponseResult result, string errorMsg, int expStatusCode)
        {
            var srvRes = new ServiceResponse<string>
            {
                Result = result,
                ErrorMessage = errorMsg
            };

            var ar = srvRes.ToActionResult().ShouldBeOfType<StatusCodeResult>();
            ar.StatusCode.ShouldBe(expStatusCode);
        }
    }
}