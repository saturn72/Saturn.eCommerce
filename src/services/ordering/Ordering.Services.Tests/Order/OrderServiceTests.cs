using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Ordering.Services.Models;
using Ordering.Services.Order;
using Saturn72.EventPublisher;
using Saturn72.EventPublisher.Events;
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
            or.Setup(o => o.GetOrdersByClientId(It.IsAny<string>())).ReturnsAsync(null as IEnumerable<OrderModel>);

            var os = new OrderService(or.Object, null);

            var res = await os.CreateOrder(order);
            res.Result.ShouldBe(ServiceResponseResult.BadOrMissingData);
        }

        [Theory]
        [MemberData(nameof(OrderService_CrateOrder_MissingDataInOrderLines_Data))]
        public async Task OrderService_CrateOrder_MissingDataInOrderLines(IEnumerable<OrderItemModel> orderItems)
        {
            var or = new Mock<IOrderRepository>();
            or.Setup(o => o.GetOrdersByClientId(It.IsAny<string>())).ReturnsAsync(null as IEnumerable<OrderModel>);

            var os = new OrderService(or.Object, null);
            var order = new OrderModel
            {
                ReferenceId = "some-reference-id",
                ClientId = "some-client-id",
                OrderItems = orderItems
            };

            var res = await os.CreateOrder(order);
            res.Result.ShouldBe(ServiceResponseResult.BadOrMissingData);
        }


        public static IEnumerable<object[]> OrderService_CrateOrder_MissingDataInOrderLines_Data => new[]
        {
            new object[] {null},
            new object[]
            {
                new OrderItemModel[] { }
            },
            new object[]
            {
                new[]
                {
                    new OrderItemModel {Sku = "some-sku"}
                }
            },
            new object[]
            {
                new[]
                {
                    new OrderItemModel {Sku = "some-sku", OrderItemId = "some-id"}
                }
            },
            //Missing reference Key
            new object[]
            {
                new[]
                {
                    new OrderItemModel
                    {
                        Sku = "some-sku",
                        OrderItemId = "some-id",
                        ReferenceIds = new Dictionary<string, string>
                        {
                            {"", "123"}
                        }
                    }
                }
            },
            //Missing reference value
            new object[]
            {
                new[]
                {
                    new OrderItemModel
                    {
                        Sku = "some-sku",
                        OrderItemId = "some-id",
                        ReferenceIds = new Dictionary<string, string>
                        {
                            {"", " "}
                        }
                    }
                }
            }
        };

        [Fact]
        public async Task OrderService_CrateOrder_MissingOrderLines()
        {
            var or = new Mock<IOrderRepository>();
            or.Setup(o => o.GetOrdersByClientId(It.IsAny<string>())).ReturnsAsync(null as IEnumerable<OrderModel>);

            var os = new OrderService(or.Object, null);
            var order = new OrderModel
            {
                ReferenceId = "some-reference-id",
                ClientId = "some-client-id"
            };

            var res = await os.CreateOrder(order);
            res.Result.ShouldBe(ServiceResponseResult.BadOrMissingData);
        }

        [Fact]
        public async Task OrderService_CrateOrder_OrderAlreadyPlaced()
        {
            var referenceId = "some-reference-id";

            var dbOrders = new[] {new OrderModel {ReferenceId = referenceId}};
            var or = new Mock<IOrderRepository>();
            or.Setup(o => o.GetOrdersByClientId(It.IsAny<string>())).ReturnsAsync(dbOrders);

            var os = new OrderService(or.Object, null);
            var order = new OrderModel
            {
                ReferenceId = referenceId,
                ClientId = "some-client-id",
                OrderItems = new[]
                {
                    new OrderItemModel
                    {
                        Sku = "some-sku",
                        OrderItemId = "some-id",
                        ReferenceIds = new Dictionary<string, string>
                        {
                            {"key", "value"}
                        }
                    }
                }
            };

            var res = await os.CreateOrder(order);
            res.Result.ShouldBe(ServiceResponseResult.NotAcceptable);
        }

        [Fact]
        public async Task OrderService_CrateOrder_PlaceOrderAndPublish()
        {
            var orderRepo = new Mock<IOrderRepository>();
            orderRepo.Setup(o => o.GetOrdersByClientId(It.IsAny<string>())).ReturnsAsync(null as OrderModel[]);

            var eventPublisher = new Mock<IEventPublisher>();

            var os = new OrderService(orderRepo.Object, eventPublisher.Object);
            var order = new OrderModel
            {
                ReferenceId = "some-reference-id",
                ClientId = "some-client-id",
                OrderItems = new[]
                {
                    new OrderItemModel
                    {
                        Sku = "some-sku",
                        OrderItemId = "some-id",
                        ReferenceIds = new Dictionary<string, string>
                        {
                            {"key", "value"}
                        }
                    },
                },
            };

            var res = await os.CreateOrder(order);
            orderRepo.Verify(r => r.CreateOrder(It.Is<OrderModel>(o => o == order)), Times.Once);
            eventPublisher.Verify(ep => ep.Publish(It.Is<CrudEvent<OrderModel>>(ev => ev.CrudEventType == CrudEventType.Created && ev.Data == order)), Times.Once);


            res.Result.ShouldBe(ServiceResponseResult.NotAcceptable);

            throw new NotImplementedException();
        }
    }
}