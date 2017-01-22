using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.DeviceDataWebInterface.Models
{
    public class DeviceBaseModel
    {
        #region Global
        public Guid Id { get; set; }
        public string DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        #endregion


        public DeviceBaseModel() : base() { }
        public DeviceBaseModel(bool dummy)
        {
            if (dummy)
            {
                if (this.Id == default(Guid)) this.Id = Guid.NewGuid();
                if (string.IsNullOrWhiteSpace(this.DeviceId)) this.DeviceId = "DUMMY";
                if (this.Timestamp == default(DateTime)) this.Timestamp = DateTime.Now;
            }
        }

    }


}
