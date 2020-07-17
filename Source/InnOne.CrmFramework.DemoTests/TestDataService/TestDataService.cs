using Autofac;
using InnOne.CrmFramework.DemoTests.TestDataService.Interfaces;

namespace InnOne.CrmFramework.DemoTests.TestDataService
{
    public class TestDataService : ITestDataService
    {
        private readonly ILifetimeScope _lifetimeScope;

        public TestDataService(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public TTestRepo GetRepository<TTestRepo>() where TTestRepo : IRepository
        {
            return _lifetimeScope.Resolve<TTestRepo>();
        }
    }
}