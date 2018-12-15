using Store.BLL.Interface;
using Store.BLL.Service;
using Store.Model;
using Store.Model.DTO;
using System;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;

namespace Store.WebApi.Controllers
{
    [RoutePrefix("api/account")]

    public class AccountController : ApiControllerBase
    {
        IAccountService _service = new AccountService();

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetAccount()
        {
            var username = GetUser();
            var accountResponse = _service.GetAccount(username);

            return accountResponse != null ? Json(ApiResult.Success(accountResponse)) : Json(ApiResult.Failure("Account Not Found"));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetAccountById(int id)
        {
            var accountResponse = _service.GetAccount(id);

            return accountResponse != null ? Json(ApiResult.Success(accountResponse)) : Json(ApiResult.Failure("Account Not Found"));
        }

        [HttpPost]
        public IHttpActionResult CreateAccount([FromBody] AccountCreateRequest dto)
        {
            try
            {
                var result = _service.CreateAccount(dto);
                return Json(ApiResult.Success());
            }
            catch (Exception ex)
            {
                return Json(ApiResult.Failure("Account creation Failed"));
            }
        }



        [HttpPut]
        public IHttpActionResult UpdateAccount(AccountUpdateRequest dto)
        {
            var username = GetUser();
            try
            {
                var result = _service.UpdateAccount(username, dto);
                return Json(ApiResult.Success());
            }
            catch (Exception ex)
            {
                return Json(ApiResult.Failure("Account update Failed"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAccount(int id)
        {
            var accountResponse = _service.GetAccount(id);

            return accountResponse != null ? Json(ApiResult.Success(accountResponse)) : Json(ApiResult.Failure("Account Not Found"));
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
            return Json(ApiResult.Success());
        }

        private string GetUser()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            return identity.FindFirst(ClaimTypes.Name).Value;
        }
    }
}
