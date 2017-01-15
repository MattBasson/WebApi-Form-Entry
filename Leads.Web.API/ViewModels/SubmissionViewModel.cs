using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Leads.Entities.Contracts;
using AutoMapper;



namespace Leads.Web.API.ViewModels
{
    public class SubmissionViewModel :  ISubmission
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool TermsAgreed { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddress { get; set; }
    }
}