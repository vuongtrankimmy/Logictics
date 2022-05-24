using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Entities.ExtendedModels.MethodErrorHandling
{
    public class Response
    {
        public int HttpStatusCode { get; set; } = 200;
        public string Title { get; set; }        
        public string Message { get; set; }
        public string Href { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public object Model { get; set; }        
    }

    public class Data
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }

    //return Ok(); // Http status code 200
    //return Created(); // Http status code 201
    //return NoContent(); // Http status code 204
    //return BadRequest(); // Http status code 400
    //return Unauthorized(); // Http status code 401
    //return Forbid(); // Http status code 403
    //return NotFound(); // Http status code 404
}
