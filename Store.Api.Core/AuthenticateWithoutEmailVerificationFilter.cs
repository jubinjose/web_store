using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace Store.Api.Core
{
    public class AuthenticateWithoutEmailVerificationFilterAttribute : Attribute, IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (!TokenHelper.TryRetrieveToken(context.Request, out string token))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", context.Request);
                    return;
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

                }
                catch (Exception e)
                {
                    context.ErrorResult = new AuthenticationFailureResult("Invalid Jwt Token", context.Request);
                }

            });
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                IPrincipal incomingPrincipal = context.ActionContext.RequestContext.Principal;
                Debug.WriteLine(String.Format("Incoming principal in custom auth filter ChallengeAsync method is authenticated: {0}", incomingPrincipal.Identity.IsAuthenticated));
            });
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}
