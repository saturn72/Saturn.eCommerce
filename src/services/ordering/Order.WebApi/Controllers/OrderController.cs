
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ordering.Services;
using Ordering.Services.Models;
using Ordering.Services.Order;

namespace Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        #region Fields

        private readonly IOrderService _orderService;

        #endregion

        #region CTOR

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        /// <summary>
        /// Creates new order
        /// </summary>
        /// <param name="order">Order details</param>
        [ProducesResponseType(typeof(OrderModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]OrderModel order)
        {
            var srvRes = await _orderService.CreateOrder(order);
            return srvRes.IsSuccessful()
                ? Created("", srvRes.Data)
                : srvRes.ToActionResult();
        }
    }
}
