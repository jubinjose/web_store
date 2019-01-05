using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Store.Api.Core
{
    public class TokenValidationHandler : DelegatingHandler
    {
        

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            //determine whether a jwt exists or not
            if (!TokenHelper.TryRetrieveToken(request, out string token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                //allow requests with no token - whether a action method needs an authentication can be set with the claimsauthorization attribute
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                //extract and assign the user of the jwt
                ClaimsPrincipal principal = TokenHelper.ValidateToken(token, out SecurityToken securityToken);

                Thread.CurrentPrincipal = principal;

                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException e)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }


    }
}