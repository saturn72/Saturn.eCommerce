using System;
using System.Collections.Generic;
using System.Net;
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
                    ServiceResponseResult.Success, data => new OkObjectResult
                        (data)
                },
                {
                    ServiceResponseResult.BadOrMissingData, data => new BadRequestObjectResult
                    (new
                    {
                        message = "Bad or missing data"
                    })
                },
                {
                    ServiceResponseResult.Created, data => new CreatedResult("", data)
                },
                {
                    ServiceResponseResult.InternalError, data => new StatusCodeResult((int) HttpStatusCode.NotAcceptable)
                }
            };

        public static IActionResult ToActionResult<TData>(this ServiceResponse<TData> serviceResponse)
        {
            return ActionResultDictionary[serviceResponse.Result](serviceResponse.Data);
        }
    }
}