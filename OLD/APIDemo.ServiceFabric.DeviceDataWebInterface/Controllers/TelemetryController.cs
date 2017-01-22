using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDemo.ServiceFabric.DeviceDataWebInterface.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/telemetry")]
    public class TelemetryController : Controller // System.Web.Http.ApiController
    {
        public Models.IDeviceTelemetryRepository Items { get; set; }
        public TelemetryController(Models.IDeviceTelemetryRepository items)
        {
            Items = items;
        }

        // GET: api/telemetry
        [HttpGet]
        public IEnumerable<Models.DeviceTelemetry> GetAll()
        {
            return Items.GetAll();           
        }

        // GET api/telemetry/3F2504E0-4F89-11D3-9A0C-0305E82C3301
        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var item =  Items.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // GET api/telemetry/last/5
        [HttpGet("lasts/{number:int}")]
        public IEnumerable<Models.DeviceTelemetry> GetLasts(int number)
        {
            return Items.GetLasts(number);             
        }

        // GET api/telemetry/last/5/Temperature
        [HttpGet("lasts/{number:int}/{type:APIDemo.ServiceFabric.DeviceDataWebInterface.Models.DeviceTelemetryType}")]
        public IEnumerable<Models.DeviceTelemetry> GetLasts(int number, Models.DeviceTelemetryType type)
        {
            return  Items.GetLasts(number, type);            
        }

        // GET api/telemetry/last/5/Temperature/Device1
        [HttpGet("lasts/{number:int}/{type:APIDemo.ServiceFabric.DeviceDataWebInterface.Models.DeviceTelemetryType}/{deviceId:string}")]
        public IEnumerable<Models.DeviceTelemetry> GetLasts(int number, Models.DeviceTelemetryType type, string deviceId)
        {
            return Items.GetLasts(number, type, deviceId);             
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
