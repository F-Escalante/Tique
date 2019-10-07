using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ServiciosController : ApiController
    {
        // GET api/servicios
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/servicios/5
        public List<Servicios> Get(int id)
        {
            List<Servicios> servicios;
            servicios = Models.Database.getServicios(id);
            return servicios;
        }

        // POST api/servicios
        public void Post([FromBody]string value)
        {
        }

        // PUT api/servicios/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/servicios/5
        public void Delete(int id)
        {
        }
    }
}
