﻿using AzureIOTApplication.Models;
using Microsoft.Azure.Amqp.Transport;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;

namespace AzureIOTApplication.Repositories
{
    public class UpdatePropertiesRepository
    {
        private readonly string _IoTHubConnectionString;
        private static string connectionString = "HostName=Jdhuba8.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=27M+VGszWeXM38MfiF51mZSu9WE413eLMu0qTY+2TVo=";
        private static string deviceConnectionstring = "HostName=Jdhuba8.azure-devices.net;DeviceId=device1;SharedAccessKey=DAw1qKyfQwj8V7fdJ/1D6IFbJ9UKQjiv0HEfNfjfxp8=";

        public UpdatePropertiesRepository(IConfiguration configuration)
        {
            _IoTHubConnectionString = configuration.GetValue<string>("IotHubConnectionString");
        }
        public static async Task<bool> IsDeviceAvailable(string deviceId)
        {
            var registrymanager = RegistryManager.CreateFromConnectionString(connectionString);
            Device device = await registrymanager.GetDeviceAsync(deviceId);
            if (device.Status == DeviceStatus.Enabled)
            {
                return true;
            }
            return false;
        } 
       
        public async Task<string> UpdateReportedPropertiesAsync(UpdateProperties properties)
        {
            var deviceClient = DeviceClient.CreateFromConnectionString(deviceConnectionstring, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            
                    var reportedProperties = new TwinCollection();
                    reportedProperties[properties.Key] = properties.Value;
                    await deviceClient.UpdateReportedPropertiesAsync(reportedProperties);
                    return "Reported Properties updated successfully";
                
   
        }
        public async Task<Object> UpdateDesiredPropertiesAsync(UpdateProperties properties, string deviceId)
        {
            var registryManager = RegistryManager.CreateFromConnectionString(_IoTHubConnectionString);

                    var twin = await registryManager.GetTwinAsync(deviceId);
            if(twin != null)
            {
                twin.Properties.Desired[properties.Key] = properties.Value;
                var response= await registryManager.UpdateTwinAsync(deviceId, twin, twin.ETag);
                return response.ToJson();
            }
            return null;
                                               
        }

    }
}