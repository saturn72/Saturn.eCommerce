using System.Collections.Generic;
using Ordering.Services.Models;

namespace Ordering.Services.Order
{
    public interface IOrderRepository
    {
        IEnumerable<OrderModel> GetOrdersByClientId(string clientId);
    }
}
