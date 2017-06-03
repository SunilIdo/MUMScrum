using Autofac;
using Autofac.Integration.Mvc;
using MUMScrum.Infrastructure.Core;
using MUMScrum.Model.Model;
using MUMScrum.Repository;
using MUMScrum.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MUMScrum.Web.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();
            

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => t.Name.EndsWith("ViewModel")).InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(EmployeeRepository).Assembly)
           .Where(t => t.Name.EndsWith("Repository"))
           .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(EmployeeRepository).Assembly)
           .Where(t => t.Name.EndsWith("Repository"))
           .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(EmployeeService).Assembly)
           .Where(t => t.Name.EndsWith("Service"))
           .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));            
        }
    }
}