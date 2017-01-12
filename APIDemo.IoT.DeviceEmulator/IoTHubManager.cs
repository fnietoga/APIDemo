using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace APIDemo.IoT.DeviceEmulator
{
    class IoTHubManager
    {
        protected static string connectionString
        {
            get
            {
                string configVal = ConfigurationManager.AppSettings["IoTHubConnectionString"];
                if (string.IsNullOrWhiteSpace(configVal))
                    throw new ConfigurationErrorsException("No se ha configurado la cadena de conexion al IoT Hub (IoTHubConnectionString)");
                return configVal;
            }
        }
    }
}
