using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Leads.Web.APInfrastructure.Helpers
{
    public static class ResponseHelper
    {
        public static HttpResponseMessage FormatMessage(string json)
        {
            var responseMessage = new HttpResponseMessage();
            var sc = new StringContent(json);
            sc.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            responseMessage.Content = sc;
            return responseMessage;
        }
    }
}