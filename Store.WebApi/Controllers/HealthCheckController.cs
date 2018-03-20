using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Store.WebApi.Controllers
{
    //If not specified, then ControllerName will be the access URL . 
    // eg:- http://localhost/Store.WebApi/api/HealthCheck/[route]
    //If specified then this route will be used eg:-  http://localhost/Store.WebApi/api/health/[route]
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

        [HttpPut]
        [AllowAnonymous]
        [Route("testPUT")]
        public string TestPUT()
        {
            return "PUT Test Success";
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("testPOST")]
        public string TestPOST()
        {
            return "POST Test Success";
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("testdelete")]
        public string TestDELETE()
        {
            return "DELETE Test Success";
        }
    }
}
