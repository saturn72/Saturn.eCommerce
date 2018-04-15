using System;
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
        [InlineData(ServiceResponseResult.BadOrMissingData, null, typeof(BadRequestObjectResult))]
        [InlineData(ServiceResponseResult.Success, "some-error-message", typeof(OkObjectResult))]
        [InlineData(ServiceResponseResult.Not, "some-error-message", typeof(OkObjectResult))]
        public async Task OrderController_Create_BadRequest_OnIllegalRequestAsync(ServiceResponseResult result, string errorMsg, Type expActionResultType)
        {
            var srvResult = new ServiceResponse<OrderModel>
            {
                Result = result,
                ErrorMessage = errorMsg
            };
            var orderSrv = new Mock<IOrderService>();

            orderSrv.Setup(s => s.CreateOrder(It.IsAny<OrderModel>())).ReturnsAsync(srvResult);

            var ctrl = new OrderController(orderSrv.Object);
            var res = await ctrl.CreateOrder(new OrderModel());

            res.ShouldBeOfType(expActionResultType);
        }
    }
}
