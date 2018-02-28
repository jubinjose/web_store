using Store.BLL.Interface;
using Store.BLL.Service;
using Store.Model;
using Store.Model.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Store.WebApi.Controllers
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
                var token = CreateToken(request.UserName);
                return Ok(OpResult<string>.SuccessResult(token));
            }

            return Ok(result);
        }

        [Route("api/auth/check")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult CheckIfLoggedIn()
        {
            return Ok("Logged in");
        }



        private string CreateToken(string userName)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName)
            });

            string sec = ConfigurationManager.AppSettings["jwtkey"];
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token = 
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:80", audience: "http://localhost:80",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
