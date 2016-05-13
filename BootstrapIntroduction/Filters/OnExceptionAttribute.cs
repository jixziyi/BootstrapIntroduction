using BootstrapIntroduction.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BootstrapIntroduction.Filters
{
    public class OnExceptionAttribute:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            var excetpionType = exceptionContext.Exception.GetType().Name;

            ReturnData returnData;

            switch (excetpionType)
            {
                case "ObjectNotFoundException":
                    returnData = new ReturnData(HttpStatusCode.NotFound,
                        exceptionContext.Exception.Message, "Error");
                    break;
                default:
                    returnData = new ReturnData(HttpStatusCode.InternalServerError,
                        "An error occurred, please try again or contact the administrator.",
                        "Error");
                    break;
            }

            exceptionContext.Controller.ViewData.Model = returnData.Content;
            exceptionContext.HttpContext.Response.StatusCode = (int)returnData.HttpStatusCode;
            exceptionContext.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = exceptionContext.Controller.ViewData
            };

            exceptionContext.ExceptionHandled = true;

            base.OnException(exceptionContext);
        }
    }
}