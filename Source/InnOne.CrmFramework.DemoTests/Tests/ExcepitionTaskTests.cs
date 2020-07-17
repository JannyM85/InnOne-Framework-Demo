using System.ServiceModel;
using Autofac;
using InnOne.CrmFramework.DemoTests.Core;
using InnOne.CrmFramework.DemoTests.TestDataService.Interfaces;
using InnOne.CrmFramework.DemoTests.TestDataService.Repos.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace InnOne.CrmFramework.DemoTests.Tests
{
    [TestClass]
    public class ExcepitionTaskTests: CrmTestBase
    {
        private readonly ITestDataService _testDataService;
        public ExcepitionTaskTests()
        {
            _testDataService = _lifeTimeScope.Resolve<ITestDataService>();
        }

        [TestCategory("ExcepitionTask")]
        [TestCategory("LeadPlugin")]
        [TestMethod]
        [ExpectedException(typeof(FaultException<OrganizationServiceFault>))]
        public void ExcepitionTaskFail()
        { 
            var lead = _testDataService.GetRepository<ILeadTestDataRepo>().GetPhoneCall();
            lead.Description = "exception";
            lead.Id = CrmTestDataService.CreateTestEntity(lead);
        }


        [TestCategory("ExcepitionTask")]
        [TestCategory("LeadPlugin")]
        [TestMethod]
        public void ExcepitionTaskPass()
        {
            var lead = _testDataService.GetRepository<ILeadTestDataRepo>().GetPhoneCall();
            lead.Description = "exception1";
            lead.Id = CrmTestDataService.CreateTestEntity(lead);
        }
    }
}