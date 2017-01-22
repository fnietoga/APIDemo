using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.ServiceFabric.DeviceDataWebInterface.Models
{
    public class DeviceTelemetry : DeviceBaseModel
    {

        #region Location
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distortion { get; set; }
        #endregion

        #region Altitude
        public double Altitude { get; set; }
        #endregion

        #region Temperature
        public string Scale { get; set; }
        public double Temperature { get; set; }
        #endregion

        #region Humidity
        public double Humidity { get; set; }
        #endregion


        public DeviceTelemetry() : base() { }
        public DeviceTelemetry(bool dummy) : this(dummy, (DeviceTelemetryType)Utils.RandomValues.GetRandomInt(0, 3)) { }
        public DeviceTelemetry(bool dummy, DeviceTelemetryType type) : base(dummy)
        {
            if (dummy)
            {
                switch (type)
                {
                    case DeviceTelemetryType.Location:
                        if (this.Latitude == default(double)) this.Latitude = Utils.RandomValues.GetRandomDouble(-90, 90);
                        if (this.Longitude == default(double)) this.Longitude = Utils.RandomValues.GetRandomDouble(-180, 180);
                        if (this.Distortion == default(double)) this.Distortion = Utils.RandomValues.GetRandomDouble(0, 5);
                        break;

                    case DeviceTelemetryType.Altitude:
                        if (this.Altitude == default(double)) this.Altitude = Utils.RandomValues.GetRandomDouble(0, 8848);
                        break;

                    case DeviceTelemetryType.Temperature:
                        if (string.IsNullOrWhiteSpace(this.Scale)) this.Scale = "celsius";
                        if (this.Temperature == default(double)) this.Temperature = Utils.RandomValues.GetRandomDouble(-40, 55);
                        break;

                    case DeviceTelemetryType.Humidity:
                        if (this.Humidity == default(double)) this.Humidity = Utils.RandomValues.GetRandomDouble(0, 100);
                        break;
                }







            }
        }
    }
}