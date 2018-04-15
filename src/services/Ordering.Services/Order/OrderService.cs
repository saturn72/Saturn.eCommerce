using System.Threading.Tasks;
using Ordering.Services.Models;

namespace Ordering.Services.Order
{
    public class OrderService : IOrderService
    {
        public Task<ServiceResponse<OrderModel>> CreateOrder(OrderModel order)
        {
            throw new System.NotImplementedException();
        }
    }
}