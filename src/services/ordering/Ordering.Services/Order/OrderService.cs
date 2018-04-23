using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ordering.Services.Events;
using Ordering.Services.Models;

namespace Ordering.Services.Order
{
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly IOrderRepository _orderRepository;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region CTOR

        public OrderService(IOrderRepository orderRepository, IEventPublisher eventPublisher)
        {
            _orderRepository = orderRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        public async Task<ServiceResponse<OrderModel>> CreateOrder(OrderModel order)
        {
            var srvRes = new ServiceResponse<OrderModel> {Data = order};
            if (!await ValidateCreateOrderModel(order, srvRes))
                return srvRes;

            await _orderRepository.CreateOrder(order);
            _eventPublisher.PublishAsync(EventType.Created, order);

            //get dropshippers
            //check if exists in dropshipper inventory
            //if exists 
            //  insert to srop shipper order q
            //  update db order status
            //if not in stock
            //  find secondary drop shipper
            //      insert to drop shipper q
            //      update order status in DB
            //
            //Setup recuring task to check order status 
            //If could not find drop shipper, send email to admin

            throw new NotImplementedException();
            srvRes.Result = ServiceResponseResult.Created;
            return srvRes;
        }

        private async Task<bool> ValidateCreateOrderModel(OrderModel order, ServiceResponse<OrderModel> srvRes)
        {
            if (order == null
                || !order.ReferenceId.HasValue()
                || !order.ClientId.HasValue())
            {
                srvRes.Result = ServiceResponseResult.BadOrMissingData;
                srvRes.ErrorMessage = "Missind Id. Please check referenceId and clientId properties";
                return false;
            }
            if (!ValidateOrderItems(order.OrderItems, srvRes))
                return false;


            var allClientOrders = await _orderRepository.GetOrdersByClientId(order.ClientId);
            var dbOrders = allClientOrders?
                .Where(ori => ori.ReferenceId.Equals(order.ReferenceId, StringComparison.InvariantCultureIgnoreCase) &&
                              ori.FulfillmentStatus != OrderFulfillmentStatus.Canceled).ToArray();

            if (dbOrders != null && dbOrders.Any())
            {
                srvRes.Result = ServiceResponseResult.NotAcceptable;
                srvRes.ErrorMessage = string.Concat(srvRes.ErrorMessage,
                    "\nOrder with the same referenceID already exists");
                return false;
            }
            return true;
        }

        private bool ValidateOrderItems(IEnumerable<OrderItemModel> orderItems,
            ServiceResponse<OrderModel> serviceResponse)
        {
            if (orderItems.IsNullOrEmpty())
            {
                serviceResponse.Result = ServiceResponseResult.BadOrMissingData;
                serviceResponse.ErrorMessage = string.Concat(serviceResponse.ErrorMessage,
                    "Missing Order lines. Please specify order items.");
                return false;
            }

            if (orderItems.Any(oi => !oi.OrderItemId.HasValue())
                || orderItems.Any(oi=>oi.ReferenceIds.IsNullOrEmpty())
                || orderItems.Any(oi => oi.ReferenceIds.Keys.Any(k=>!k.HasValue()))
                || orderItems.Any(oi => oi.ReferenceIds.Values.Any(v=>!v.HasValue()))
                || orderItems.Any(oi => !oi.Sku.HasValue()))
            {
                serviceResponse.Result = ServiceResponseResult.BadOrMissingData;
                serviceResponse.ErrorMessage = string.Concat(serviceResponse.ErrorMessage,
                    "Missing Order lines data. Please specify all required data in order items (check all identifiers).");
                return false;
            }
            return true;
        }
    }
}