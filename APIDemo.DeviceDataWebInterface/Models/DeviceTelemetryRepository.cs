using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

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

    public class DeviceTelemetryRepository : DocumentDBContext, IDeviceTelemetryRepository
    {
        protected override string CollectionName { get { return "devicedataCollection"; } }
             

        public DeviceTelemetry Get(Guid id)
        {
            using (DocumentClient client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var query = client.CreateDocumentQuery<DeviceTelemetry>(CollectionUri,
                    new SqlQuerySpec(
                        "SELECT * FROM Telemetry t WHERE t.id = @id",
                        new SqlParameterCollection() { new SqlParameter("@id", id.ToString()) }),
                    new FeedOptions() { MaxItemCount = 1 });
                return query.AsEnumerable().FirstOrDefault();
            }

            //return new DeviceTelemetry(true) { Id = id };
        }

        public IEnumerable<DeviceTelemetry> GetAll()
        {
            using (DocumentClient client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var query = client.CreateDocumentQuery<DeviceTelemetry>(CollectionUri,
                    new FeedOptions() { MaxItemCount = -1 });
                return query.ToList();
            }

            //List<DeviceTelemetry> retVal = new List<DeviceTelemetry>();
            //for (int i = 1; i <= 10; i++)
            //{
            //    retVal.Add(new DeviceTelemetry(true));
            //} 
        }

        public IEnumerable<DeviceTelemetry> GetLasts(int number)
        {
            using (DocumentClient client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var query = client.CreateDocumentQuery<DeviceTelemetry>(CollectionUri,
                    new SqlQuerySpec(string.Format("SELECT TOP {0} * FROM Telemetry t ORDER BY t.timestamp DESC", number)));
                return query.ToList();
            }

            //List<DeviceTelemetry> retVal = new List<DeviceTelemetry>();
            //for (int i = 1; i <= number; i++)
            //{
            //    retVal.Add(new DeviceTelemetry(true));
            //}
            //return retVal;
        }

        public IEnumerable<DeviceTelemetry> GetLasts(int number, DeviceTelemetryType type)
        {

            using (DocumentClient client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var query = client.CreateDocumentQuery<DeviceTelemetry>(CollectionUri,
                   new SqlQuerySpec(string.Format(GetQuerySpecByDeviceTelemetryType(type) + " ORDER BY t.timestamp DESC", number)));
                return query.ToList();
            }

            //List<DeviceTelemetry> retVal = new List<DeviceTelemetry>();
            //for (int i = 1; i <= number; i++)
            //{
            //    retVal.Add(new DeviceTelemetry(true, type) { });
            //}
            //return retVal;
        }

        public IEnumerable<DeviceTelemetry> GetLasts(int number, DeviceTelemetryType type, string deviceId)
        {
            using (DocumentClient client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var query = client.CreateDocumentQuery<DeviceTelemetry>(CollectionUri,
                   new SqlQuerySpec(string.Format(GetQuerySpecByDeviceTelemetryType(type) + " AND t.deviceId = @deviceId " + " ORDER BY t.timestamp DESC",number),
                    new SqlParameterCollection() { new SqlParameter("@deviceId", deviceId) }));
                return query.ToList();
            }

            //List<DeviceTelemetry> retVal = new List<DeviceTelemetry>();
            //for (int i = 1; i <= number; i++)
            //{
            //    retVal.Add(new DeviceTelemetry(true, type) { DeviceId = deviceId });
            //}
            //return retVal;
        }

        private string GetQuerySpecByDeviceTelemetryType(DeviceTelemetryType type)
        {
            switch (type)
            {
                case DeviceTelemetryType.Location:
                    return "SELECT TOP {0} * FROM Telemetry t WHERE (is_defined(t.longitude) OR is_defined(t.latitude))";

                case DeviceTelemetryType.Altitude:
                    return "SELECT TOP {0} * FROM Telemetry t WHERE is_defined(t.altitude)";

                case DeviceTelemetryType.Humidity:
                    return "SELECT TOP {0} * FROM Telemetry t WHERE is_defined(t.humidity)";

                case DeviceTelemetryType.Temperature:
                    return "SELECT TOP {0} * FROM Telemetry t WHERE is_defined(t.temperature)";

                default:
                    return "SELECT TOP {0} * FROM Telemetry t";
            }
        }
    }
}
