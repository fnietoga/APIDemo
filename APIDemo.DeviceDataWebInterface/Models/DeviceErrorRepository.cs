using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.DeviceDataWebInterface.Models
{
    public interface IDeviceErrorRepository
    {
        IEnumerable<DeviceError> GetAll();
        DeviceError Get(Guid id); 
        IEnumerable<DeviceError> GetLasts(int number);
        IEnumerable<DeviceError> GetLasts(int number, int errorNumber);
        IEnumerable<DeviceError> GetLasts(int number, string deviceId);
    }
    public class DeviceErrorRepository : IDeviceErrorRepository
    {
        public DeviceError Get(Guid id)
        {
           return new DeviceError(true) { Id = id };
        }

        public IEnumerable<DeviceError> GetAll()
        {
            List<DeviceError> retVal = new List<DeviceError>();
            for (int i = 1; i <= 10; i++)
            {
                retVal.Add(new DeviceError(true));
            }
            return retVal;
        }

        public IEnumerable<DeviceError> GetLasts(int number)
        {
            List<DeviceError> retVal = new List<DeviceError>();
            for (int i = 1; i <= number; i++)
            {
                retVal.Add(new DeviceError(true));
            }
            return retVal;
        }

        public IEnumerable<DeviceError> GetLasts(int number, int errorNumber)
        {
            List<DeviceError> retVal = new List<DeviceError>();
            for (int i = 1; i <= number; i++)
            {
                retVal.Add(new DeviceError(true) { ErrorNumber = errorNumber});
            }
            return retVal;
        }

        public IEnumerable<DeviceError> GetLasts(int number, string deviceId)
        {
            List<DeviceError> retVal = new List<DeviceError>();
            for (int i = 1; i <= number; i++)
            {
                retVal.Add(new DeviceError(true) { DeviceId = deviceId });
            }
            return retVal;
        }
    }
}
