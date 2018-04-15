using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ordering.Services;

namespace Order.WebApi
{
    public static class ServiceResponseExtensions
    {
        private static readonly IDictionary<ServiceResponseResult, Func<object, IActionResult>> ActionResultDictionary
            = new Dictionary<ServiceResponseResult, Func<object, IActionResult>>
            {
                {
                    ServiceResponseResult.BadOrMissingData, data => new BadRequestObjectResult
                    (new{
                        message = "Bad or missing data"
                    })
                },
                {ServiceResponseResult.Success, data => new OkObjectResult(new
                {
                    message = "Sucess with error message",
                    data = data
                })}
            };

        public static IActionResult ToActionResult<TData>(this ServiceResponse<TData> serviceResponse)
        {
            return serviceResponse.Result == ServiceResponseResult.Success
                ? new OkObjectResult(serviceResponse.Data)
                : ActionResultDictionary[serviceResponse.Result](serviceResponse.Data);
        }
    }
}