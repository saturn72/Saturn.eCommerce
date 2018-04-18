using System.Collections.Generic;

namespace Ordering.Services.Models
{
    public class OrderModel
    {
        public string ClientId { get; set; }
        public string OrderId { get; set; }
        public string ReferenceId { get; set; }
        public IEnumerable<OrderItemModel> OrderItems { get; set; }
    }
}
