using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Leads.Web.APInfrastructure.Contracts;
using Leads.Web.APInfrastructure.Helpers;
using Leads.Web.API.ViewModels.Response;
using Newtonsoft.Json;
using Ninject;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Leads.Web.APInfrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationRequiredAttribute : ActionFilterAttribute{}
}