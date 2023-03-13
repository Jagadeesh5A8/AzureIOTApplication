using AzureIOTApplication.Models;
using AzureIOTApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;

namespace AzureIOTApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private static DeviceRepository repository;
        public DeviceController()
        {
            repository = new DeviceRepository();
        }
        [HttpPost]
        [Route("create")]
        public async Task<string> AddDeviceAsync(IOTDevice iOTDevice)
        {
            return await repository.AddDeviceAsync(iOTDevice);
        }
        [HttpGet]
        [Route("Retrive/(id)")]
        public async Task<Device> GetDeviceAsync(string id)
        {
            return await repository.GetDeviceAsync(id);
        }
        [HttpPut]
        [Route("Update/(id)/(status)")]
        public async Task UpdateDeviceStatusAsync(string id, string status)
        {
            await repository.UpdateDeviceStatusAsync(id, status);
        }
        [HttpDelete]
        [Route("Delete/(id)")]
        public async Task DeleteDeviceAsync(string id)
        {
            await repository.DeleteDeviceAsync(id);
        }

    }
}

 
