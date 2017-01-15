using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Leads.Web.API.ViewModels.Response
{
    public class WithoutDataResponse
    {
        /// <summary>
        /// Gets or sets the response meta data.
        /// </summary>
        /// <value>
        /// The response meta data.
        /// </value>
        [JsonProperty(PropertyName = "meta")]
        public MetaDataResponse ResponseMetaData { get; set; }
    }
}