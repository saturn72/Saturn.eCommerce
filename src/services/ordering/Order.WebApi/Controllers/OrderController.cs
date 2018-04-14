
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ordering.Services.Models;

namespace Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        /// <summary>
        /// Creates new order
        /// </summary>
        /// <param name="order">Order details</param>
        [ProducesResponseType(typeof(OrderModel), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]OrderModel order)
        {
            throw new System.NotImplementedException();
        }
    }
}
