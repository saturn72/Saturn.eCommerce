using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ordering.Services;

namespace Order.WebApi
{
    public static class ServiceResponseExtensions
    {
        private static readonly IDictionary<ServiceResponseResult, Func<object, string, IActionResult>>
            ActionResultDictionary
                = new Dictionary<ServiceResponseResult, Func<object, string, IActionResult>>
                {
                    {
                        ServiceResponseResult.Success, (data, errMsg) => new OkObjectResult
                            (data)
                    },
                    {
                        ServiceResponseResult.BadOrMissingData, (data, errMsg) => new BadRequestObjectResult
                        (new
                        {
                            message = errMsg ?? "Bad or missing data",
                            model = data
                        })
                    },
                    {
                        ServiceResponseResult.Created, (data, errMsg) => new CreatedResult("", data)
                    },
                    {
                        ServiceResponseResult.InternalError,
                        (data, errMsg) => new StatusCodeResult((int) HttpStatusCode.NotAcceptable)
                    }
                };

        public static IActionResult ToActionResult<TData>(this ServiceResponse<TData> serviceResponse)
        {
            return ActionResultDictionary[serviceResponse.Result](serviceResponse.Data, serviceResponse.ErrorMessage);
        }
    }
}