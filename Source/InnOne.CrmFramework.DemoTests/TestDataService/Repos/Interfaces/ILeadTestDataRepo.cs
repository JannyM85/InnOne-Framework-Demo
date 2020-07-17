using InnOne.CrmFramework.DemoTests.TestDataService.Interfaces;

namespace InnOne.CrmFramework.DemoTests.TestDataService.Repos.Interfaces
{
    extern alias Demo;

    public interface ILeadTestDataRepo : IRepository
    {
        Demo::InnOne.CrmFramework.Demo.Lead GetPhoneCall();
    }
}