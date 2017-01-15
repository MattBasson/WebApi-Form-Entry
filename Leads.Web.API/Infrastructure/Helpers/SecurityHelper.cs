using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Leads.Web.APInfrastructure.Contracts;

namespace Leads.Web.APInfrastructure.Helpers
{
    public class SecurityHelper
    {

        //private readonly IApplicationSettings _settings;

        //public SecurityHelper()
        //{
        //    //_settings = settings;
        //}
        /// <summary>
        /// Checks if a token is valid.
        /// </summary>
        /// <param name="token">string - generated either by GenerateToken() or via client with cryptojs etc.</param>
        /// <param name="ip">string - IP address of client, passed in by RESTAuthenticate attribute on controller.</param>
        /// <returns>bool</returns>
        public bool IsTokenValid(string token, string ip)
        {
            var isValid = string.Equals(ConfigurationManager.AppSettings["Application.ApiPublicKey"], token);
            return isValid;
        }
    }
}