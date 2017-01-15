using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;


using Leads.Web.APInfrastructure.Contracts;

namespace Leads.Web.APInfrastructure.Settings
{
    public class ApplicationSettings:BaseSettings,IApplicationSettings
    {

        public ApplicationSettings() { }

        public ApplicationSettings(NameValueCollection settings)
            : base(settings)
        {
        }

        public string ApiPublicKey => GetSetting("ApiPublicKey", string.Empty);
        public string Origin => GetSetting("Origin", string.Empty);

        public DateTime CloseDate
        {
            get
            {
                var dateString = GetSetting("CloseDate", string.Empty);
                var format = DateFormat;
                return DateTime.ParseExact(dateString,format,null);
            }
        }

        public string DateFormat => GetSetting("DateFormat", string.Empty);
    }
}