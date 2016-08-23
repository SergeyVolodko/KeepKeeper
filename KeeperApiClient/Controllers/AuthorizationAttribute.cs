using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json.Linq;

namespace KeeperApiClient.Controllers
{
    public class MyAuthFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {

            if (!context.Request.Headers.Contains("Authorization"))
            {
                throw new AuthenticationException();
            }

            var token = context.Request.Headers.Authorization.Parameter;

            var content = JWT.JsonWebToken.Decode(token, "secret");

            var a = content;
        }
    }
}