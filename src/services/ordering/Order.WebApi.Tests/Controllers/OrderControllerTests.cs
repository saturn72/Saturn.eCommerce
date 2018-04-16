using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Order.WebApi.Controllers;
using Ordering.Services;
using Ordering.Services.Models;
using Ordering.Services.Order;
using Shouldly;
using Xunit;

namespace Order.WebApi.Tests.Controllers
{
    public class OrderControllerTests
    {
        [Theory]
        [InlineData(ServiceResponseResult.InternalError, typeof(StatusCodeResult), HttpStatusCode.NotAcceptable)]
        [InlineData(ServiceResponseResult.BadOrMissingData, typeof(BadRequestObjectResult), HttpStatusCode.BadRequest)]
        [InlineData(ServiceResponseResult.Created, typeof(CreatedResult), HttpStatusCode.Created)]
        public async Task OrderController_Create_BadRequest_OnIllegalRequestAsync(ServiceResponseResult result, Type expResultType, HttpStatusCode expStatusCode)
        {
            var srvResult = new ServiceResponse<OrderModel>
            {
                Result = result,
            };
            var orderSrv = new Mock<IOrderService>();

            orderSrv.Setup(s => s.CreateOrder(It.IsAny<OrderModel>())).ReturnsAsync(srvResult);

            var ctrl = new OrderController(orderSrv.Object);
            var res = await ctrl.CreateOrder(new OrderModel());

            res.ShouldBeOfType(expResultType);

            if(expResultType == typeof(StatusCodeResult))
                (res as StatusCodeResult).StatusCode.ShouldBe((int)expStatusCode);
        }
    }
}
