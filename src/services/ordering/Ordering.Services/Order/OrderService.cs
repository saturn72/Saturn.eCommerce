using System;
using System.Linq;
using System.Threading.Tasks;
using Ordering.Services.Models;

namespace Ordering.Services.Order
{
    public class OrderService : IOrderService
    {
        public async Task<ServiceResponse<OrderModel>> CreateOrder(OrderModel order)
        {
            var srvRes = new ServiceResponse<OrderModel>();
            if (!ValidateCreateOrderModel(order, srvRes))
                return srvRes;
            throw new System.NotImplementedException();
        }

        private bool ValidateCreateOrderModel(OrderModel order, ServiceResponse<OrderModel> srvRes)
        {
            if (order == null
                || !order.ReferenceId.HasValue()
                || !order.ClientId.HasValue())
            {
                srvRes.Result = ServiceResponseResult.BadOrMissingData;
                srvRes.ErrorMessage = "Missind Id. Please check referenceId and clientId properties";
            }
            if (order?.OrderItems == null
                || !order.OrderItems.Any())
            {
                srvRes.Result = ServiceResponseResult.BadOrMissingData;
                srvRes.ErrorMessage = (srvRes.ErrorMessage ?? "") + " Missind Order lines. Please specify order items.";
            }
            if(checked if Order Id already exists. ==> clientid_referenceId)
            return srvRes.IsSuccessful();
        }
    }
}