using Microsoft.AspNetCore.Mvc;
using SOLID.Principles.Demo.Models;
using SOLID.S.SingleResponsibility.Service;

namespace SOLID.S.SingleResponsibility.Controllers
{
    [ApiController]
    public class SingleResponsibilityController : ControllerBase
    {
       private readonly IOrderService _orderService;

        public SingleResponsibilityController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("api/[controller]")]
        [HttpPost]
        public IActionResult ProcessOrder(Order order)
        {
                return Ok(_orderService.ProcessOrder(order));
        }
    }
}
