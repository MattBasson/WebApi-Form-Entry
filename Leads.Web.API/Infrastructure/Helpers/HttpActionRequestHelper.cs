using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Leads.Web.APInfrastructure.Helpers
{
    public static class HttpActionRequestHelper
    {
        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            return request.Properties.ContainsKey("MS_HttpContext") ? IPAddress.Parse((request.Properties["MS_HttpContext"] as HttpContextBase).Request.UserHostAddress).ToString() : null;
        }
    }
}