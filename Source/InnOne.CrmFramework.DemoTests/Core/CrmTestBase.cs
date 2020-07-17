using Autofac;
using Innone.CrmFramework.Autofac.CrmTest;
using InnOne.CrmFramework.DemoTests.DeleteHandlers;
using InnOne.CrmFramework.DemoTests.TestDataService.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InnOne.CrmFramework.DemoTests.Core
{
    public class CrmTestBase : CrmTestBaseAf
    {
        protected readonly ITestDataService TestData;

        public CrmTestBase()
        {
            TestData = _lifeTimeScope.Resolve<ITestDataService>();
            
            CrmTestDataService.AddCleanUpDeleteHanlder(new LeadDeleteHandler());
        }

        protected override void RegisterAutofacModules(ContainerBuilder builder)
        {
            builder.RegisterModule<TestAutofacModule>();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            CrmTestDataService.DeleteTestEntities();
        }
    }
}