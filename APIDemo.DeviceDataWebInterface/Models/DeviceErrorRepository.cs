using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

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
    public class DeviceErrorRepository : DocumentDBContext, IDeviceErrorRepository
    {
        protected override string CollectionName { get { return "deviceErrorsCollection"; } }


        public DeviceError Get(Guid id)
        {
            using (DocumentClient client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var query = client.CreateDocumentQuery<DeviceError>(CollectionUri,
                    new SqlQuerySpec(
                        "SELECT * FROM Errors e WHERE e.id = @id",
                        new SqlParameterCollection() { new SqlParameter("@id", id.ToString()) }),
                    new FeedOptions() { MaxItemCount = 1 });
                return query.AsEnumerable().FirstOrDefault();
            }
            //return new DeviceError(true) { Id = id };
        }

        public IEnumerable<DeviceError> GetAll()
        {
            using (DocumentClient client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var query = client.CreateDocumentQuery<DeviceError>(CollectionUri,
                    new FeedOptions() { MaxItemCount = -1 });
                return query.ToList();
            }

            //List<DeviceError> retVal = new List<DeviceError>();
            //for (int i = 1; i <= 10; i++)
            //{
            //    retVal.Add(new DeviceError(true));
            //}
            //return retVal;
        }

        public IEnumerable<DeviceError> GetLasts(int number)
        {
            using (DocumentClient client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var query = client.CreateDocumentQuery<DeviceError>(CollectionUri,
                    new SqlQuerySpec(string.Format("SELECT TOP {0} * FROM Errors e ORDER BY e.timestamp DESC", number)));
                return query.ToList();
            }

            //List<DeviceError> retVal = new List<DeviceError>();
            //for (int i = 1; i <= number; i++)
            //{
            //    retVal.Add(new DeviceError(true));
            //}
            //return retVal;
        }

        public IEnumerable<DeviceError> GetLasts(int number, int errorNumber)
        {
            using (DocumentClient client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var query = client.CreateDocumentQuery<DeviceError>(CollectionUri,
                   new SqlQuerySpec(string.Format("SELECT TOP {0} * FROM Errors e WHERE e.errorNumber = @errorNumber ORDER BY e.timestamp DESC", number),
                    new SqlParameterCollection() { new SqlParameter("@errorNumber", errorNumber) }));
                return query.ToList();
            }

            //List<DeviceError> retVal = new List<DeviceError>();
            //for (int i = 1; i <= number; i++)
            //{
            //    retVal.Add(new DeviceError(true) { ErrorNumber = errorNumber});
            //}
            //return retVal;
        }

        public IEnumerable<DeviceError> GetLasts(int number, string deviceId)
        {
            using (DocumentClient client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var query = client.CreateDocumentQuery<DeviceError>(CollectionUri,
                   new SqlQuerySpec(string.Format("SELECT TOP {0} * FROM Errors e WHERE e.deviceId = @deviceId ORDER BY e.timestamp DESC", number),
                    new SqlParameterCollection() { new SqlParameter("@deviceId", deviceId) }));
                return query.ToList();
            }

            //List<DeviceError> retVal = new List<DeviceError>();
            //for (int i = 1; i <= number; i++)
            //{
            //    retVal.Add(new DeviceError(true) { DeviceId = deviceId });
            //}
            //return retVal;
        }
    }
}
