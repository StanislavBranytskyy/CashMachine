using Autofac;
using Autofac.Integration.Mvc;
using CashMachine.BusinessLayer.Services.Abstraction;
using CashMachine.BusinessLayer.Services.Concrete;
using CashMachine.Model.DAL;
using CashMachine.Model.DAL.Repositories.Abstraction;
using CashMachine.Model.DAL.Repositories.Concrete;
using System.Web.Mvc;

namespace CashMachine.Utils
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CashMachineContext>().InstancePerLifetimeScope();

            builder.RegisterType<CreditCardRepository>().As<ICreditCardRepository>().InstancePerRequest();
            builder.RegisterType<CreditCardService>().As<ICreditCardService>().InstancePerRequest();

            builder.RegisterType<OperationsRepository>().As<IOperationsRepository>().InstancePerRequest();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            using (var scope = container.BeginLifetimeScope())
            {
                var context = scope.Resolve<CashMachineContext>();
                if (!context.Database.Exists())
                {
                    context.Database.Initialize(true);
                }
            }
        }
    }
}