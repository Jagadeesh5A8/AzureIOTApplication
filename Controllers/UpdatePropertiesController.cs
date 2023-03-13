using AzureIOTApplication.Models;
using AzureIOTApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureIOTApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatePropertiesController : ControllerBase
    {
        private readonly UpdatePropertiesRepository repository;
        public UpdatePropertiesController(UpdatePropertiesRepository repository) { 
        this.repository = repository;

        }
        [HttpPost("reported")]
        public async Task<IActionResult> UpdateReportedProperties([FromBody] UpdateProperties model)
        {
            var twin = await repository.UpdateReportedPropertiesAsync(model);
            return Ok(twin);

        }
        [HttpPost("desired")]
        public async Task<IActionResult> UpdateDesiredProperties([FromBody] UpdateProperties model ,  string deviceId)
        {
            var twin = await repository.UpdateDesiredPropertiesAsync(model, deviceId);
            return Ok(twin);
        }
    }
}
