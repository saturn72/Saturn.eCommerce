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
            if (!ValidateCreateOrder(order, srvRes))
                return srvRes;
                throw new System.NotImplementedException();
        }

        private bool ValidateCreateOrder(OrderModel order, ServiceResponse<OrderModel> srvRes)
        {
            if (order?.ReferenceIds == null
                || !order.ReferenceIds.Any()
                || string.IsNullOrEmpty(order.ClientId)
                || string.IsNullOrWhiteSpace(order.ClientId))
            {
                srvRes.Result = ServiceResponseResult.BadOrMissingData;
                srvRes.ErrorMessage = "Missind Id. Please check referenceId and clientId properties";
            }

            return srvRes.IsSuccessful();
        }
    }
}