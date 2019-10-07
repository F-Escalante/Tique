using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CategoriasController : ApiController
    {
        // GET api/categorias
        public List<Categorias> Get()
        {
            List<Categorias> categorias;
            categorias = Models.Database.getCategorias();
            return categorias;
        }

        // GET api/categorias/5
        public List<Categorias> Get(int id)
        {
            List<Categorias> categorias;
            categorias = Models.Database.getCategorias(id);
            return categorias;
            
        }

        // POST api/categorias
        public void Post([FromBody]string value)
        {
        }

        // PUT api/categorias/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/categorias/5
        public void Delete(int id)
        {
        }
    }
}
