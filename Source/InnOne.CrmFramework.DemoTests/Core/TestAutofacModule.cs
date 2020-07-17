using System.Linq;
using Autofac;
using Innone.CrmFramework.CrmDataService.Interfaces;
using InnOne.CrmFramework.DemoTests.TestDataService.Interfaces;
using Module = Autofac.Module;

namespace InnOne.CrmFramework.DemoTests.Core
{
    public class TestAutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(TestAutofacModule).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IRepository))))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(TestAutofacModule).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(ICrmDataRepository))))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(TestAutofacModule).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IService))))
                .AsImplementedInterfaces();
        }
    }
}