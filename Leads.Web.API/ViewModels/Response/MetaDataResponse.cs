using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace Leads.Web.API.ViewModels.Response
{
    public class MetaDataResponse
    {
        private static string _localeName;
        /// <summary>
        ///     Used to serialise the strings in a locale. Should NOT be set when an error has occured (All error messages are in
        ///     en).
        /// </summary>
        [JsonIgnore]
        public CultureInfo Culture { get; set; }

        /// <summary>
        ///     The 2 char locale of the response. Assumed to match the requested locale.
        ///     This property is not returned in the event of an error.
        /// </summary>
        [JsonProperty(PropertyName = "locale", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortLocale
        {
            get { return Culture != null ? Culture.TwoLetterISOLanguageName : _localeName; }
            set { _localeName = value; }
        }

        /// <summary>
        ///     Used to track the respose code of the request.
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public HttpStatusCode? ResponseCode { get; set; }

        /// <summary>
        ///     Shown only when the request has failed. Does not appear to be localised.
        ///     Current implimentation does not exactly match the string returned by the API, a lookup table will be required.
        /// </summary>
        [JsonProperty(PropertyName = "error_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorType
        {
            get
            {
                if (ResponseCode.HasValue && ResponseCode.Value == HttpStatusCode.Unauthorized)
                    return "Unauthorised";

                return ResponseCode.HasValue && ResponseCode.Value != HttpStatusCode.OK
                    ? "No Response"
                    : null;
            }
        }

        /// <summary>
        ///     Shown only when the request has failed. Does not appear to be localised. NOT user friendly.
        /// </summary>
        [JsonProperty(PropertyName = "error_message", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }
    }
}