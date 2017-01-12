using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace APIDemo.IoT.DeviceEmulator
{
    class DeviceManagement : IoTHubManager
    {
      
      
        static RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);

        internal static List<Device> RegisterDevices(int number)
        {
            List<Device> retVal = new List<Device>(); 

            Task.Run(
                async () =>
                {
                    try
                    {
                        var devices = await registryManager.GetDevicesAsync(Int32.MaxValue);
                        int n = 1;
                        foreach (Device curDevice in devices)
                        {
                            if (n <= number)
                                retVal.Add(curDevice);
                            else
                                await DeleteDeviceAsync(curDevice.Id);
                            n++;
                        }
                        for (int i = n; i <= number; i++)
                        {
                            retVal.Add(
                                await AddDeviceAsync(string.Format("Device{0}", i))
                            );
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Oops, {0}", ex.Message);
                    }
                }
            ).GetAwaiter().GetResult();

            return retVal;
        }

        internal static void DeleteAllDevices()
        { 
           
            Task.Run(
                async () =>
                {
                    try
                    {
                        var devices = await registryManager.GetDevicesAsync(Int32.MaxValue);
                        foreach (Device curDevice in devices)
                        {
                            await DeleteDeviceAsync(curDevice.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Oops, {0}", ex.Message);
                    }
                }
            ).GetAwaiter().GetResult(); 
        }

        private static async Task<Device> AddDeviceAsync(string deviceId)
        {

            Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
            return device;
        }

        private static async Task DeleteDeviceAsync(string deviceId)
        {
            Device device = await registryManager.GetDeviceAsync(deviceId);
            try
            {
                await registryManager.RemoveDeviceAsync(deviceId);
            }
            catch (DeviceNotFoundException)
            {
            }
            Console.WriteLine("Removed device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
