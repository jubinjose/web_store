﻿using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc.Filters;

namespace Store.Web.Core
{
    public class AuthenticateWithoutEmailVerificationFilterAttribute : Attribute, IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                IPrincipal incomingPrincipal = context.ActionContext.RequestContext.Principal;
                Debug.WriteLine(String.Format("Incoming principal in custom auth filter AuthenticateAsync method is authenticated: {0}", incomingPrincipal.Identity.IsAuthenticated));
                IPrincipal genericPrincipal = new GenericPrincipal(new GenericIdentity("Andras", "CustomIdentification"), new string[] { "Admin", "PowerUser" });
                context.Principal = genericPrincipal;
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
