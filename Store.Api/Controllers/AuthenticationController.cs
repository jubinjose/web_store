﻿using Store.Model;
using Store.Model.DTO;
using System.Web.Http;
using Store.Api.Models;
using Store.Api.Core;
using System.Collections.Generic;
using Store.Service;

namespace Store.Api.Controllers
{
    public class AuthenticationController : ApiController
    {
        IAuthenticationService _service = new AuthenticationService();

        [Route("api/auth/login")]
        [HttpPost]
        [AllowAnonymous()]
        public IHttpActionResult LogOn([FromBody] LoginRequest request)
        {
            var result = _service.LogOn(request);

            if (result.Success)
            {
                var account = ((OpResult<Account>)result).Result;

                string token;

                if (string.IsNullOrWhiteSpace(account.EmailVerificationCode))
                {
                    token = CreateFullyAuthorizedToken(request.UserName);
                }
                else
                {
                    token = CreateEmailVerificationRequiredToken(request.UserName);
                }

                return Json(ApiResult.Success(new { jwt = token }));
            }

            return Json(ApiResult.Failure(result.Errors));
        }

        [Route("api/auth/check")]
        [HttpGet]
        [AuthenticateWithoutEmailVerificationFilter]
        //[Authorize]
        public IHttpActionResult CheckIfLoggedIn()
        {
            return Ok("Logged in");
        }


        private string CreateFullyAuthorizedToken(string userName)
        {
            return TokenHelper.GetInstance().CreateToken(
                new Dictionary<string, string>
                {
                    { "username", userName}
                }
            );
        }

        private string CreateEmailVerificationRequiredToken(string userName)
        {
            return TokenHelper.GetInstance().CreateToken(
                new Dictionary<string, string>
                {
                    { "username", userName},
                    { "activationrequired", "true"}
                }
            );
        }

        
    }
}
