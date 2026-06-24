using Microsoft.AspNetCore.Mvc;
using SOLID.Principles.Demo.Models;
using SOLID.Principles.Demo.Services;

namespace SOLID.Principles.Demo.Controllers
{
    [ApiController]
    public class OpenClosePrincipleController : ControllerBase
    {
        public IOpenClosePrincipleService _openClosePrincipleService { get; set; }

        public OpenClosePrincipleController(IOpenClosePrincipleService openClosePrincipleService)
        {
            _openClosePrincipleService = openClosePrincipleService;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult Post(Order order)
        {
            var discountedAmount = _openClosePrincipleService.ApplyDiscount(order);
            return Ok(discountedAmount);
        }
    }
}
