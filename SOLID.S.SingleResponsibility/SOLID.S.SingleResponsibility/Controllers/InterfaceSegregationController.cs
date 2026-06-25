using Microsoft.AspNetCore.Mvc;
using SOLID.Principles.Demo.Models;
using SOLID.Principles.Demo.Services;

namespace SOLID.Principles.Demo.Controllers
{
    [ApiController]
    public class InterfaceSegregationController : ControllerBase
    {
        private readonly IInterfaceSegregationService _interfaceSegregationService;
        public InterfaceSegregationController(IInterfaceSegregationService interfaceSegregationService)
        {
            _interfaceSegregationService = interfaceSegregationService;
        }

        [HttpPost("NotifyCustomer")]
        public IActionResult NotifyCustomer([FromBody] Customer customer)
        {
            var result = _interfaceSegregationService.Notify(customer);
            return Ok(result);
        }
    }
}
