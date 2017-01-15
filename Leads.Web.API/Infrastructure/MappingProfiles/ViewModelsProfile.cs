using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Leads.Web.API.ViewModels;
using AutoMapper;
using Leads.Entities.Submissions;

namespace Leads.Web.APInfrastructure.MappingProfiles
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<SubmissionViewModel, Submission>()
                .ForMember(m => m.CreatedDate, opt => opt.UseValue(DateTime.Now));
        }
    }
}