using System;
using System.Collections.Generic;
using System.Web.Http;

namespace APIDemo.DeviceDataWebInterface.Controllers
{
    [ServiceRequestActionFilter]
    [RoutePrefix("api/errors")]
    public class ErrorsController : ApiController
    {
        public Models.IDeviceErrorRepository Items { get; set; }

        public ErrorsController(Models.IDeviceErrorRepository items) { Items = items; }
        public ErrorsController() : this(new Models.DeviceErrorRepository()) { }

        // GET: api/errors
        [Route("")]
        public IEnumerable<Models.DeviceError> Get()
        { 
            return Items.GetAll();
        }

        // GET api/errors/3F2504E0-4F89-11D3-9A0C-0305E82C3301
        [Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            var item = Items.Get(id);
            if (item == null || id == Guid.Empty)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // GET api/errors/lasts/5
        [Route("lasts/{number:int}")]
        public IEnumerable<Models.DeviceError> GetLasts(int number)
        { 
            return Items.GetLasts(number);
        }

        // GET api/errors/lasts/5/333
        [Route("lasts/{number:int}/{errorNumber:int}")]
        public IEnumerable<Models.DeviceError> GetLasts(int number, int errorNumber)
        {
            return Items.GetLasts(number, errorNumber);         
        }

        // GET api/errors/lasts/5/Device1
        [Route("lasts/{number:int}/{deviceId}")]
        public IEnumerable<Models.DeviceError> GetLasts(int number, string deviceId)
        {
            return Items.GetLasts(number, deviceId);
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
