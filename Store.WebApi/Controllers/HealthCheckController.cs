using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Store.WebApi.Controllers
{
    //If not specified, then ControllerName will be the access URL . eg:- http://localhost/Store.WebApi/api/HealthCheck/ping
    //If specified , http://localhost/Store.WebApi/api/health/ping
    [RoutePrefix("api/health")]

    public class HealthCheckController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("ping")]
        public string Ping()
        {
            return "Hello! Time is " + DateTime.Now.ToShortDateString();
        }
    }
}
