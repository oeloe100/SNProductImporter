using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.OAuth.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Providers
{
    public class NopOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            var endpointPath = new PathString("/NopAuthorize");

            if (context.Options.TokenEndpointPath == endpointPath)
            {
                context.MatchesAuthorizeEndpoint();
                Trace.WriteLine(context.OwinContext.Request.QueryString);
            }

            return base.MatchEndpoint(context);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            Trace.WriteLine(context);
            return base.ValidateClientRedirectUri(context);
        }

        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            Trace.WriteLine(context);
            return base.ValidateAuthorizeRequest(context);
        }

        public override Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            Trace.WriteLine(context);
            return base.AuthorizeEndpoint(context);
        }
    }
}