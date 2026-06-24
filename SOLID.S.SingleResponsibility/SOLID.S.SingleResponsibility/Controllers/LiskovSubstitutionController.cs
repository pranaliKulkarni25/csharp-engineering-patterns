using Microsoft.AspNetCore.Mvc;
using SOLID.Principles.Demo.Models;
using SOLID.Principles.Demo.Services;

namespace SOLID.Principles.Demo.Controllers
{
    [ApiController]
    public class LiskovSubstitutionController : ControllerBase
    {
        private readonly ILiskovSubstitutionService _liskovSubstitution;
        public LiskovSubstitutionController(ILiskovSubstitutionService liskovSubstitution)
        {
            _liskovSubstitution = liskovSubstitution;
        }

        [HttpPost("ProcessPayment")]
        public IActionResult Process([FromBody] Order order)
        {
            var result = _liskovSubstitution.processPayment(order);
            return Ok(result);
        }

        [HttpPost("ProcessRefund")]
        public IActionResult Refund(string paymentMethod, Guid transactionId)
        {
            var result = _liskovSubstitution.RefundPayment(paymentMethod, transactionId);
            return Ok(result);
        }

    }

}
