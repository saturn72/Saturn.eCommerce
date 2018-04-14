using System.Threading.Tasks;
using Order.WebApi.Controllers;
using Ordering.Services.Models;
using Xunit;

namespace Order.WebApi.Tests.Controllers
{
    public class OrderControllerTests
    {
        [Theory]
        [InlineData(null)]
        public async Task OrderController_Create_BadRequest_OnIllegalRequestAsync(OrderModel order)
        {
            var ctrl = new OrderController();
            var res = await ctrl.CreateOrder(order);
            throw new System.NotImplementedException();
        }
    }
}
