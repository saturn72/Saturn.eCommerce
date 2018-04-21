using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Ordering.Services.Models;
using Ordering.Services.Order;
using Shouldly;
using Xunit;

namespace Ordering.Services.Tests.Order
{
    public class OrderServiceTests
    {
        public static IEnumerable<object[]> OrderService_CrateOrder_MissingIds_Data => new[]
        {
            new object[] {null},
            new object[]
            {
                new OrderModel()
            },
            new object[]
            {
               new OrderModel {ReferenceId = "123"}
            },
            new object[]
            {
                new OrderModel {ClientId = "123"}
            }
        };

        [Theory]
        [MemberData(nameof(OrderService_CrateOrder_MissingIds_Data))]
        public async Task OrderService_CrateOrder_MissingIds(OrderModel order)
        {
            var or = new Mock<IOrderRepository>();
            or.Setup(o => o.GetOrdersByClientId(It.IsAny<string>())).Returns(null as IEnumerable<OrderModel>);

            var os = new OrderService(or.Object);

            var res = await os.CreateOrder(order);
            res.Result.ShouldBe(ServiceResponseResult.BadOrMissingData);
        }

        [Fact]
        public async Task OrderService_CrateOrder_MissingOrderLines()
        {
            var or = new Mock<IOrderRepository>();
            or.Setup(o => o.GetOrdersByClientId(It.IsAny<string>())).Returns(null as IEnumerable<OrderModel>);

            var os = new OrderService(or.Object);
            var order = new OrderModel
            {
                ReferenceId ="some-reference-id",
                ClientId = "some-client-id",
            };

            var res = await os.CreateOrder(order);
            res.Result.ShouldBe(ServiceResponseResult.BadOrMissingData);
        }
    }
}