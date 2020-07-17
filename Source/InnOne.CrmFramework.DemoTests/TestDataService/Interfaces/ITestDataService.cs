namespace InnOne.CrmFramework.DemoTests.TestDataService.Interfaces
{
    public interface ITestDataService : IService
    {
        TTestRepo GetRepository<TTestRepo>() where TTestRepo : IRepository;
    }
}