
using System.Web.Http;
using System.Web.Http.Filters;
using Leads.Web.APInfrastructure.Attributes;
using Leads.Web.APInfrastructure.Filters;
using Microsoft.Ajax.Utilities;
using Ninject.Web.WebApi.FilterBindingSyntax;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Leads.Web.API.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Leads.Web.API.NinjectWebCommon), "Stop")]

namespace Leads.Web.API
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using System.Linq;
    using Ninject.Web.WebApi.FilterBindingSyntax;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.BindHttpFilter<AuthorizationRequiredFilter>(FilterScope.Controller)
                    .WhenControllerHas<AuthorizationRequiredAttribute>();
                RegisterServices(kernel);

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>    
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(scan =>
            {
                
                scan.FromAssembliesMatching("Leads*")
                    .SelectAllClasses()
                    .BindDefaultInterface();
                

            });
            ////Not sure I need this.
            //kernel.Bind<IDbContext>().To<WinFreeFuelDbContext>();

            //var bindings = kernel.GetBindings(typeof(IApplicationSettings));
        }        
    }
}
