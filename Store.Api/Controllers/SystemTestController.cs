using System;
using System.Web.Http;

namespace Store.Api.Controllers
{
    //If RoutePrefix not specified, then ControllerName will be the access URL . 
    // eg:- http://localhost/Store.WebApi/api/SystemTest/[route]
    //If RoutePrefix is specified, then that route will be used eg:-  http://localhost/Store.WebApi/api/test/[route]
    [RoutePrefix("api/test")]

    public class SystemTestController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("get")]
        public string Ping()
        {
            return "GET Test Success - " + DateTime.Now.ToShortDateString();
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("put")]
        public string TestPUT()
        {
            return "PUT Test Success";
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("post")]
        public string TestPOST()
        {
            return "POST Test Success";
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("delete")]
        public string TestDELETE()
        {
            return "DELETE Test Success";
        }
    }
}
