using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDemo.ServiceFabric.DeviceDataWebInterface.Controllers
{
    [Route("api/[controller]")]
    public class ErrorsController : Controller
    {
        public Models.IDeviceErrorRepository Items { get; set; }
        public ErrorsController(Models.IDeviceErrorRepository items)
        {
            Items = items;
        }

        // GET: api/errors
        [HttpGet]
        public IEnumerable<Models.DeviceError> Get()
        { 
            return Items.GetAll();
        }

        // GET api/errors/3F2504E0-4F89-11D3-9A0C-0305E82C3301
        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var items = Items.Get(id);
            if (items == null)
            {
                return NotFound();
            }
            return new ObjectResult(items);
        }

        // GET api/errors/lasts/5
        [HttpGet("lasts/{number:int}")]
        public IEnumerable<Models.DeviceError> GetLasts(int number)
        { 
            return Items.GetLasts(number);
        }

        // GET api/errors/lasts/5/333
        [HttpGet("lasts/{number:int}/{errorNumber:int}")]
        public IEnumerable<Models.DeviceError> GetLasts(int number, int errorNumber)
        {
            return Items.GetLasts(number, errorNumber);         
        }

        // GET api/errors/lasts/5/Device1
        [HttpGet("lasts/{number:int}/{deviceId}")]
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
