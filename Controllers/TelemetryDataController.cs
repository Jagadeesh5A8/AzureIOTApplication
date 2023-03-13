using AzureIOTApplication.Models;
using AzureIOTApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureIOTApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryDataController : ControllerBase
    {
        private readonly TelemetryDataRepository _repository;
        public TelemetryDataController()
        {
            _repository = new TelemetryDataRepository();
        }
        [HttpPost]
        public async Task<IActionResult> PostTelemetryData(string deviceId, TelemetryData telemetryData)
        {
            try
            {
                var result = await _repository.SendTelemetryData(deviceId, telemetryData);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
