using System;

namespace Leads.Web.APInfrastructure.Contracts
{
    public interface IApplicationSettings
    {
        string ApiPublicKey { get; }

        string Origin { get; }

        DateTime CloseDate {get;}

        string DateFormat { get; }
    }
}