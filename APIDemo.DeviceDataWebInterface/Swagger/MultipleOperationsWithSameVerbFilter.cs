using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.Swagger;
using System.Web.Http.Description;

namespace APIDemo.DeviceDataWebInterface
{
    public class MultipleOperationsWithSameVerbFilter : IOperationFilter
    {
        public void Apply(
            Operation operation,
            SchemaRegistry schemaRegistry,
            ApiDescription apiDescription)
        {
            if (operation.parameters != null)
            {
                operation.operationId += "By";
                foreach (var parm in operation.parameters)
                {
                    operation.operationId += string.Format("{0}", parm.name);
                }
            }
        }
    }
}
