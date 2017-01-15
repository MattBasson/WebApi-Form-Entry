using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Leads.Web.APInfrastructure.Contracts;
using Leads.Web.APInfrastructure.Helpers;
using Leads.Web.API.ViewModels.Response;
using Newtonsoft.Json;
using Ninject.Web.WebApi.Filter;


namespace Leads.Web.APInfrastructure.Filters
{
    public class AuthorizationRequiredFilter:AbstractActionFilter
    {
        private const string Token = "Token";
        private readonly IApplicationSettings _settings;
        public AuthorizationRequiredFilter(IApplicationSettings settings)
        {
            _settings = settings;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains(Token))
            {
                var tokenValue = actionContext.Request.Headers.GetValues(Token).First();

                var ip = actionContext.Request.GetClientIpAddress();
                if (!IsTokenValid(tokenValue, ip))
                {
                    var responseMessage = ResponseHelper.FormatMessage(JsonConvert.SerializeObject(new ErrorResponse
                    {
                        ResponseMetaData = new MetaDataResponse
                        {
                            Culture = CultureInfo.CurrentCulture,
                            ResponseCode = HttpStatusCode.Unauthorized,
                            ErrorMessage = "Access not allowed."
                        }
                    }));

                    actionContext.Response = responseMessage;
                }
            }
            else
            {
                var responseMessage = ResponseHelper.FormatMessage(JsonConvert.SerializeObject(new ErrorResponse
                {
                    ResponseMetaData = new MetaDataResponse()
                    {
                        Culture = CultureInfo.CurrentCulture,
                        ResponseCode = HttpStatusCode.Unauthorized,
                        ErrorMessage = "Access not allowed."
                    }
                }));

                actionContext.Response = responseMessage;
            }

            base.OnActionExecuting(actionContext);
        }

        public override bool AllowMultiple => false;

        private bool IsTokenValid(string token, string ip)
        {
            var isValid = string.Equals(_settings.ApiPublicKey, token);
            return isValid;
        }
    }
}