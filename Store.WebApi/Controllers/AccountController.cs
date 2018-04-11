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
        public IHttpActionResult CreateAccount([FromBody] AccountCreateRequest dto)
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

            if (accountDTO != null) return Ok(OpResult<AccountResponse>.SuccessResult(accountDTO));

            return NotFound("Account Not Found");
        }

        [HttpPut]
        public IHttpActionResult UpdateAccount(AccountUpdateRequest dto)
        {
            try
            {
                var result = _service.UpdateAccount(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(OpResult.FailureResult("Account Creation Failed"));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteAccount(int id)
        {
            _service.DeleteAccount(id);
            return Ok(OpResult.SuccessResult());
        }
    }
}
