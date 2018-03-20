using Store.Model;
using Store.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Store.WebApi.Controllers
{
    [RoutePrefix("api/account")]

    public class AccountController : ApiController
    {
        //[Route("get")]
        //public HttpResponseMessage Get()
        //{
        //    var result = new string[] { "value1", "value2" };
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        List<Person> personList = new List<Person>
        {
                new Person {ID = 1, FirstName = "jubin", LastName = "jose" , Email = "jubin.jose@gmail.com"},
                new Person { ID = 2,FirstName = "charmaine", LastName = "korah" , Email = "charmainerk@gmail.com" },
                new Person { ID = 3,FirstName = "alayna", LastName = "joseph" , Email = "alaynajoseph@gmail.com" }
        };

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var person = personList.FirstOrDefault((p) => p.ID == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public IHttpActionResult CreateAccount([FromBody] AccountCreateDto dto)
        {
            return Ok(dto);
        }


        public void Put(int id, [FromBody]string value)
        {

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var person = personList.FirstOrDefault((p) => p.ID == id);
            if (person == null)
            {
                return NotFound();
            }
            personList.Remove(person);
            return Ok(person);
        }
    }
}
