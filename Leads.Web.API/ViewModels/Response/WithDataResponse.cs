using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Leads.Web.API.ViewModels.Response
{
    public class WithDataResponse<T>
    {
        /// <summary>
        /// Gets or sets the response meta data.
        /// </summary>
        /// <value>
        /// The response meta data.
        /// </value>
        [JsonProperty(PropertyName = "meta")]
        public MetaDataResponse MetaDataResponse { get; set; }

        /// <summary>
        /// Gets or sets the response data of type.
        /// </summary>
        /// <value>
        /// The response data of type.
        /// </value>
        [JsonProperty(PropertyName = "data")]
        public IEnumerable<T> ResponseData { get; set; }
    }
}