using Store.BLL.Service;
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
        List<Profile> personList = new List<Profile>
        {
                new Profile {ID = 1, FirstName = "jubin", LastName = "jose" },
                new Profile { ID = 2,FirstName = "charmaine", LastName = "korah"  },
                new Profile { ID = 3,FirstName = "alayna", LastName = "joseph"  }
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
        [Route("create")]
        public IHttpActionResult CreateAccount([FromBody] AccountCreateDto dto)
        {
            var salt = Jubin.Utility.EncryptionUtility.GenerateRandomSaltString();
            var hashedPass = Jubin.Utility.EncryptionUtility.GeneratePBKDF2Hash(dto.Password, salt);
            Account acc = new Account
            {
                UserName = dto.UserName,
                PasswordHash = hashedPass,
                PasswordSalt = salt,
                Email = dto.Email
            };
            AccountService service = new AccountService();
            var result = service.CreateAccount(acc);
            if (result.exception == null)
            {
                return Ok(result);
            }
            else
            {
                return Ok(OpResult.FailureResult("Account Creation Failed"));
            }
        }

        [HttpPost]
        public IHttpActionResult ResetPassword([FromBody] string userName)
        {
            return Ok(true);
        }

        [HttpPost]
        public IHttpActionResult ForgotUserName([FromBody] string eMail)
        {
            return Ok(true);
        }


        public void Put(int id, [FromBody] string value)
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
