using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ordering.Services;
using Shouldly;
using Xunit;

namespace Order.WebApi.Tests
{
    public class ServiceResponseExtensionsTests
    {
        [Theory]
        [InlineData(ServiceResponseResult.BadOrMissingData, typeof(BadRequestObjectResult))]
        [InlineData(ServiceResponseResult.Success, typeof(OkObjectResult))]
        [InlineData(ServiceResponseResult.Created, typeof(CreatedResult))]
        [InlineData(ServiceResponseResult.NotAcceptable, typeof(ObjectResult))]
        public void ServiceResponseExtensions_ToActionResult_Generic(ServiceResponseResult result, Type expObjectResult)
        {
            var srvRes = new ServiceResponse<string>
            {
                Result = result,
            };

            var ar = srvRes.ToActionResult();

            ar.ShouldBeOfType(expObjectResult);
        }

        [Theory]
        [InlineData(ServiceResponseResult.InternalError, HttpStatusCode.InternalServerError)]
        public void ServiceResponseExtensions_ToActionResult_StatusCode(ServiceResponseResult result, HttpStatusCode expStatusCode)
        {
            var srvRes = new ServiceResponse<string>
            {
                Result = result,
            };

            var ar = srvRes.ToActionResult().ShouldBeOfType<StatusCodeResult>()
                ;
            ar.StatusCode.ShouldBe((int)expStatusCode);
        }

    }
}