using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Leads.Entities.Base;
using Leads.Entities.Contracts;


namespace Leads.Entities.Submissions
{
    public class Submission : EntityBase, ISubmission
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
    
        public bool TermsAgreed { get; set; }
     
        public DateTime CreatedDate { get; set; }

        public string IpAddress { get; set; }
    }
}
