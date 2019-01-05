using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Api.Core
{
    public class TokenHelper
    {
        public string JwtKey { get; private set; }

        private static TokenHelper _instance;

        private TokenHelper() { }

        // Not really a singleton, because we want init to be called with the jwtkey before we can ever use this class
        public static TokenHelper GetInstance()
        {
            if (_instance == null) throw new Exception("First call init with jwt key");
            return _instance;
        }

        public static void Init(string jwtKey)
        {
            if (_instance == null)
            {
                _instance = new TokenHelper() { JwtKey = jwtKey };
            }
        }

        public static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        public static ClaimsPrincipal ValidateToken(string token, out SecurityToken securityToken1)
        {
            var now = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(TokenHelper.GetInstance().JwtKey));


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidAudience = "http://localhost:80",
                ValidIssuer = "http://localhost:80",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                LifetimeValidator = TokenHelper.LifetimeValidator,
                IssuerSigningKey = securityKey
            };

            var claimsPrincipal = handler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
            securityToken1 = securityToken;
            return claimsPrincipal;
        }

        public static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, 
            TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }

        public string CreateToken(Dictionary<string, string> claims)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();
            

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims.Select(d => new Claim(d.Key, d.Value))
                );

            string sec = this.JwtKey;
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
