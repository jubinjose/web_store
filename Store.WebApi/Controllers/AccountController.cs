using Store.BLL.Interface;
using Store.BLL.Service;
using Store.Model;
using Store.Model.DTO;
using System;
using System.Web.Http;

namespace Store.WebApi.Controllers
{
    [RoutePrefix("api/account")]

    public class AccountController : ApiControllerBase
    {
        IAccountService _service = new AccountService();

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateAccount([FromBody] AccountCreateDto dto)
        {
            try
            {
                var result = _service.CreateAccount(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(OpResult.FailureResult("Account Creation Failed"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAccount(int id)
        {
            var accountDTO = _service.GetAccount(id);

            if (accountDTO != null) return Ok(OpResult<AccountDTO>.SuccessResult(accountDTO));

            return NotFound("Account Not Found");
        }

        [HttpDelete]
        public IHttpActionResult DeleteAccount(int id)
        {
            var result = _service.DeleteAccount(id);

            if (result)
            {
                return Ok(OpResult.SuccessResult());
            }

            return NotFound("Account Not Found");

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

        
    }
}
