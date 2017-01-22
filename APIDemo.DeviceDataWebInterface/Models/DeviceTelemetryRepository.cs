using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.DeviceDataWebInterface.Models
{
    public interface IDeviceTelemetryRepository
    {
        IEnumerable<DeviceTelemetry> GetAll();
        DeviceTelemetry Get(Guid id);       
        IEnumerable<DeviceTelemetry> GetLasts(int number);
        IEnumerable<DeviceTelemetry> GetLasts(int number, DeviceTelemetryType type);
        IEnumerable<DeviceTelemetry> GetLasts(int number, DeviceTelemetryType type, string deviceId);
    }

    public class DeviceTelemetryRepository : IDeviceTelemetryRepository
    {
        public DeviceTelemetry Get(Guid id)
        {
            return new DeviceTelemetry(true) { Id = id };
        }

        public IEnumerable<DeviceTelemetry> GetAll()
        {
            List<DeviceTelemetry> retVal = new List<DeviceTelemetry>();
            for (int i = 1; i <= 10; i++)
            {
                retVal.Add(new DeviceTelemetry(true));
            }
            return retVal;
        }

        public IEnumerable<DeviceTelemetry> GetLasts(int number)
        {
            List<DeviceTelemetry> retVal = new List<DeviceTelemetry>();
            for (int i = 1; i <= number; i++)
            {
                retVal.Add(new DeviceTelemetry(true));
            }
            return retVal;
        }

        public IEnumerable<DeviceTelemetry> GetLasts(int number, DeviceTelemetryType type)
        {
            List<DeviceTelemetry> retVal = new List<DeviceTelemetry>();
            for (int i = 1; i <= number; i++)
            {
                retVal.Add(new DeviceTelemetry(true, type) { });
            }
            return retVal;
        }

        public IEnumerable<DeviceTelemetry> GetLasts(int number, DeviceTelemetryType type, string deviceId)
        {
            List<DeviceTelemetry> retVal = new List<DeviceTelemetry>();
            for (int i = 1; i <= number; i++)
            {
                retVal.Add(new DeviceTelemetry(true, type) { DeviceId = deviceId });
            }
            return retVal;
        }
    }
}
