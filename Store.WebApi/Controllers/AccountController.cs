using Store.BLL.Interface;
using Store.BLL.Service;
using Store.Model;
using Store.Model.DTO;
using System;
using System.Security.Claims;
using System.Threading;
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
        [Authorize]
        public IHttpActionResult GetAccount()
        {
            var username = GetUser();
            var accountResponse = _service.GetAccount(username);

            if (accountResponse != null) return Ok(OpResult<AccountResponse>.SuccessResult(accountResponse));

            return NotFound("Account Not Found");
        }

        [HttpPut]
        public IHttpActionResult UpdateAccount(AccountUpdateRequest dto)
        {
            var username = GetUser();
            try
            {
                var result = _service.UpdateAccount(username, dto);
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
            var accountResponse = _service.GetAccount(id);

            if (accountResponse != null) return Ok(OpResult<AccountResponse>.SuccessResult(accountResponse));

            return NotFound("Account Not Found");
        }

        //[HttpPut]
        //public IHttpActionResult UpdateAccount(AccountUpdateRequest dto)
        //{
        //    try
        //    {
        //        var result = _service.UpdateAccount(dto);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(OpResult.FailureResult("Account Creation Failed"));
        //    }
        //}


        [HttpDelete]
        public IHttpActionResult DeleteAccount(int id)
        {
            _service.DeleteAccount(id);
            return Ok(OpResult.SuccessResult());
        }

        private string GetUser()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            return identity.FindFirst(ClaimTypes.Name).Value;
        }
    }
}
