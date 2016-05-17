﻿using BootstrapIntroduction.Filters;
using System.Web;
using System.Web.Mvc;

namespace BootstrapIntroduction
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new OnExceptionAttribute());
            filters.Add(new BasicAuthenticationAttribute());
        }
    }
}