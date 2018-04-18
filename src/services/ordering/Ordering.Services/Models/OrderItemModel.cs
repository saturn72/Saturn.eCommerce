using System.Collections.Generic;

namespace Ordering.Services.Models
{
    public class OrderItemModel
    {
        public IDictionary<string, string> ReferenceIds { get; set; }
        public string OrderItemId { get; set; }
        public string Sku { get; set; }
        public IEnumerable<string> Upcs { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
        public decimal Quantity { get; set; }
    }
}