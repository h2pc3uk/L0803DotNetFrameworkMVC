﻿using System.Web;
using System.Web.Mvc;

namespace L0803DotNetFrameworkMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}