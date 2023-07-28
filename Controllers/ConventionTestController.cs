using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfoCity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Solo se aplica si no esta habilitado los atributos de manera local
    [ApiConventionType(typeof(DefaultApiConventions))]
    //Permite separar las especificaciones de cada API
    [ApiExplorerSettings(GroupName = "DowloadPictures.API")]
    public class ConventionTestController : ControllerBase
    {
        // GET: api/<ConventionTestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ConventionTestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ConventionTestController>
        [HttpPost]
        //Solo se aplica si no esta habilitado los atributos de manera local
        [ApiConventionMethod(typeof(CustomConventions),nameof(CustomConventions.Insert))]
        public void Insert([FromBody] string value)
        {
        }

        // PUT api/<ConventionTestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConventionTestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
