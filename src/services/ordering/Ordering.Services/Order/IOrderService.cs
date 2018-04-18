using System.Threading.Tasks;
using Ordering.Services.Models;

namespace Ordering.Services.Order
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderModel>> CreateOrder(OrderModel order);
    }
}
