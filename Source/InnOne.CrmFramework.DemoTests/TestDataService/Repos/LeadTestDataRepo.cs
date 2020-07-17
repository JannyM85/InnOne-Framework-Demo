extern alias Demo;
using System;
using Demo::InnOne.CrmFramework.Demo;
using InnOne.CrmFramework.DemoTests.TestDataService.Repos.Interfaces;

namespace InnOne.CrmFramework.DemoTests.TestDataService.Repos
{
    public class LeadTestDataRepo : ILeadTestDataRepo
    {
        public Lead GetPhoneCall()
        {
            return new Lead
            {
                Subject = $"Test-{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}",
                FirstName = "Test1",
                LastName = "Test2",
                MobilePhone = "123123",
                Telephone1 = "222333"
            };
        }
    }
}