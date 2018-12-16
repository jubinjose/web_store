using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Store.Api.Controllers
{
    public abstract class ApiControllerBase: ApiController
    {
        protected IHttpActionResult NotFound(string message)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, message));
        }
    }
}