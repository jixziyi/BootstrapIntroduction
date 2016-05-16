using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Security.Principal;
using System.Web.Mvc.Filters;
using System.Text;
using BootstrapIntroduction.Models;
using System.Web.Mvc;

namespace BootstrapIntroduction.Filters
{
    public class BasicAuthentcationAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var authorization = request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorization) || !authorization.Contains("Basic"))
            {
                return;
            }

            byte[] encodedDataAsBytes = Convert.FromBase64String(
                authorization.Replace("Basic", ""));

            string value = Encoding.ASCII.GetString(encodedDataAsBytes);

            string username = value.Substring(0, value.IndexOf(':'));
            string password = value.Substring(value.IndexOf(':') + 1);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                filterContext.Result = new HttpUnauthorizedResult("Username or password missing");
                return;
            }

            var user = AuthenticatedUsers.Users
                .FirstOrDefault(u => u.Name == username && u.Password == password);

            if (user == null)
            {
                filterContext.Result = new HttpUnauthorizedResult("Invalid username and password");
                return;
            }

            filterContext.principal = new GenericPrincipal(user, user.Roles);
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            filterContext.Result = new BasicChallengeActionResult
            {
                CurrentResult = filterContext.Result
            };
        }
    }

    public class BasicChallengeActionResult : ActionResult
    {
        public ActionResult CurrentResult { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            CurrentResult.ExecuteResult(context);

            var response = context.HttpContext.Response;

            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                response.AddHeader("WWW-Authenticate", "Basic");
            }
        }
    }
}