using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public HttpResponseMessage Post([FromBody]LoginEntrada loginEntrada)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "La consulta no trajo datos");
            LoginSalida login = new LoginSalida();
            login = Models.Database.getLogin(loginEntrada.email, loginEntrada.nombre, loginEntrada.telefono, loginEntrada.device_id);
            if (login.roles != null)
            {
                response = Request.CreateResponse<LoginSalida>(HttpStatusCode.OK, login);
            }
            return response;
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public HttpResponseMessage Delete([FromBody]LoginEntrada loginEntrada)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se pudo desloguear");
            bool deslogueo;
            deslogueo = Models.Database.desloguear(loginEntrada.email, loginEntrada.device_id);
            if(deslogueo == true) 
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Usuario deslogueado");
            }


            return response;
        }
    }
}
