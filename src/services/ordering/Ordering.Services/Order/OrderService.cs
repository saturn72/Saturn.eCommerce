using System;
using System.Linq;
using System.Threading.Tasks;
using Ordering.Services.Models;

namespace Ordering.Services.Order
{
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly IOrderRepository _orderRepository;

        #endregion

        #region CTOR

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        #endregion

        public async Task<ServiceResponse<OrderModel>> CreateOrder(OrderModel order)
        {
            var srvRes = new ServiceResponse<OrderModel> {Data = order};
            if (!ValidateCreateOrderModel(order, srvRes))
                return srvRes;
            //insert to db with status created
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
        }

        private bool ValidateCreateOrderModel(OrderModel order, ServiceResponse<OrderModel> srvRes)
        {
            if (order == null
                || !order.ReferenceId.HasValue()
                || !order.ClientId.HasValue())
            {
                srvRes.Result = ServiceResponseResult.BadOrMissingData;
                srvRes.ErrorMessage = "Missind Id. Please check referenceId and clientId properties";
                return false;
            }
            if (order?.OrderItems == null
                || !order.OrderItems.Any())
            {
                srvRes.Result = ServiceResponseResult.BadOrMissingData;
                srvRes.ErrorMessage = string.Concat(srvRes.ErrorMessage,
                    "\nMissing Order lines. Please specify order items.");
            }

            var dbOrders = _orderRepository.GetOrdersByClientId(order.ClientId)?
                .Where(ori => ori.ReferenceId.Equals(order.ReferenceId, StringComparison.InvariantCultureIgnoreCase) &&
                              ori.FulfillmentStatus != OrderFulfillmentStatus.Canceled).ToArray();

            if (dbOrders != null && dbOrders.Any())
            {
                srvRes.Result = ServiceResponseResult.NotAcceptable;
                srvRes.ErrorMessage = string.Concat(srvRes.ErrorMessage,
                    "\nOrder with the same referenceID already exists");
            }

            return srvRes.IsSuccessful();
        }
    }
}