using System.Collections.Generic;
using System.Threading.Tasks;
using Ordering.Services.Models;

namespace Ordering.Services.Order
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetOrdersByClientId(string clientId);
        Task<OrderModel> CreateOrder(OrderModel order);
    }
}
