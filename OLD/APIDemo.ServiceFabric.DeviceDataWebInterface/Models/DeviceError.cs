using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.ServiceFabric.DeviceDataWebInterface.Models
{
    public class DeviceError : DeviceBaseModel
    {
        #region Error
        public int ErrorNumber { get; set; }
        public string ErrorDescription { get; set; }
        #endregion

        public DeviceError() : base() { }
        public DeviceError(bool dummy) : base(dummy)
        {
            if (dummy)
            {
                if (this.ErrorNumber == default(int)) this.ErrorNumber = Utils.RandomValues.GetRandomInt(0, 999);
                if (string.IsNullOrWhiteSpace(this.ErrorDescription)) this.ErrorDescription = string.Format("Random message for error # {0}.", this.ErrorNumber);
            }
        }

    }
}
