using System;
using System.Collections.Generic;
using System.Web.Http;

namespace APIDemo.DeviceDataWebInterface.Controllers
{
    [ServiceRequestActionFilter]
    [RoutePrefix("api/telemetry")]
    public class TelemetryController : ApiController
    {
        public Models.IDeviceTelemetryRepository Items { get; set; }

        public TelemetryController(Models.IDeviceTelemetryRepository items) { Items = items; }
        public TelemetryController() : this(new Models.DeviceTelemetryRepository()) { }

        // GET api/telemetry 
        [Route("")]
        public IEnumerable<Models.DeviceTelemetry> Get()
        {
            return Items.GetAll();
        }

        // GET api/telemetry/3F2504E0-4F89-11D3-9A0C-0305E82C3301
        [Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            if (id == null || id == Guid.Empty) return BadRequest();

            var item = Items.Get(id);

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // GET api/telemetry/last/5
        [Route("lasts/{number:int}")]
        public IEnumerable<Models.DeviceTelemetry> GetLasts(int number)
        {
            return Items.GetLasts(number);
        }

        // GET api/telemetry/last/5/Temperature
        [Route("lasts/{number:int}/{type}")]
        public IEnumerable<Models.DeviceTelemetry> GetLasts(int number, Models.DeviceTelemetryType type)
        {
            return Items.GetLasts(number, type);
        }

        // GET api/telemetry/last/5/Temperature/Device1
        [Route("lasts/{number:int}/{type}/{deviceId}")]
        public IEnumerable<Models.DeviceTelemetry> GetLasts(int number, Models.DeviceTelemetryType type, string deviceId)
        {
            return Items.GetLasts(number, type, deviceId);
        }

        //// POST api/telemetry 
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/telemetry/5 
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/telemetry/5 
        //public void Delete(int id)
        //{
        //}
    }
}
