using System;

namespace Leads.Entities.Contracts
{
    public interface ISubmission
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        
        bool TermsAgreed { get; set; }

        DateTime CreatedDate { get; set; }

        string IpAddress { get; set; }

    }
}