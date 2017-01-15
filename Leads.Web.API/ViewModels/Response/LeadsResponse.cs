using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leads.Entities.Submissions;


namespace Leads.Web.API.ViewModels.Response
{
    public class LeadsResponse : WithSingleDataResponse<Submission>
    {
    }
}