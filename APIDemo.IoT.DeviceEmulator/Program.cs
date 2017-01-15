using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Configuration;

namespace APIDemo.IoT.DeviceEmulator
{
    class Program
    {

        //static DeviceClient deviceClient;
        //static string iotHubUri = "IntegrationIoTHub.azure-devices.net";
        static List<Device> Devices {
            get;
            set;
        }
        static int DevicesNumber
        {
            get
            {
                string configVal = ConfigurationManager.AppSettings["devicesNumber"];
                if (string.IsNullOrWhiteSpace(configVal))
                    throw new ConfigurationErrorsException("No se ha configurado el número de dispositivos deseado en IoT Hub (devicesNumber)");
                return int.Parse(configVal);
            }
        }

        static void Main(string[] args)
        {
            //Register devices           
            Devices = DeviceManagement.RegisterDevices(DevicesNumber);
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) { 
                //On cancel, delete all devices
                //DeviceManagement.DeleteAllDevices();
                e.Cancel = true;
                Environment.Exit(0);
            };

            //Emulate Devices Messages        
            MessageManagement.EmulateDeviceToCloudMessagesAsync(Devices);     

        }
    }
}
