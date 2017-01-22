using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDemo.DeviceDataWebInterface.Models
{
    public class DocumentDBContext
    {
        protected string EndpointUri { get { return "https://ddbintegrationdevicedata.documents.azure.com:443/"; } }
        protected string PrimaryKey { get { return "Osmru4yFKYttzd3gIcrMUlqmmscJRD20JtHhrUuXya6OEouwjSAhefCe9ync86Kh6WQP3JBzh37j9vzIzqFtFw=="; } }
        protected string DatabaseName { get { return "devicedataDB"; } }
        protected virtual string CollectionName { get { return string.Empty; } }

        protected Uri CollectionUri { get { return UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName); } }
    }
}
