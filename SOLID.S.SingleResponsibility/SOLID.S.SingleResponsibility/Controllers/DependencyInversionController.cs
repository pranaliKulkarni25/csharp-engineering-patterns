using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOLID.Principles.Demo.Services;

namespace SOLID.Principles.Demo.Controllers
{
    public class DependencyInversionController : ControllerBase
    {
        private readonly IDependencyInversionService _dependencyInversionService;

        public DependencyInversionController(IDependencyInversionService dependencyInversionService)
        {
            _dependencyInversionService = dependencyInversionService;
        }

        [HttpPost("GenerateReport")]
        public IActionResult GenerateReport(string format, int requestDataCount)
        {
            var result = _dependencyInversionService.GenerateReport(format, requestDataCount);
            return Ok(result);
        }
    }
}
