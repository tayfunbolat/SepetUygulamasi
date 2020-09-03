using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CicekSepeti.Api
{
    public class RemoteFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            AuthUser authUser = new AuthUser();

            authUser.RequestIp = context.HttpContext.Connection.RemoteIpAddress.ToString();

            AuthUser.Current = authUser;

            base.OnActionExecuting(context);
        }
    }
}
