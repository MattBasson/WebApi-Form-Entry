using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Leads.Web.API.App_Start
{
    public class MapperConfig
    {
        public static void Configure()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where( x=>x.FullName.Contains("Leads")).ToArray();
            Mapper.Initialize(cfg => 
            cfg.AddProfiles(assemblies));
            
        }
    }
}