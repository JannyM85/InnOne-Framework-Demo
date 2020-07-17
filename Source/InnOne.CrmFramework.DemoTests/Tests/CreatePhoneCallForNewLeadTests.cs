extern alias Demo;
using System;
using System.Linq;
using Autofac;
using Demo::InnOne.CrmFramework.Demo;
using Innone.Core.Cache;
using InnOne.CrmFramework.DemoTests.Core;
using InnOne.CrmFramework.DemoTests.Crm.Interfaces;
using InnOne.CrmFramework.DemoTests.TestDataService.Interfaces;
using InnOne.CrmFramework.DemoTests.TestDataService.Repos.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace InnOne.CrmFramework.DemoTests.Tests
{
    extern alias Demo;

    [TestClass]
    public class CreatePhoneCallForNewLeadTests : CrmTestBase
    {
        private readonly ITestDataService _testDataService;
        public CreatePhoneCallForNewLeadTests()
        {
            _testDataService = _lifeTimeScope.Resolve<ITestDataService>();
        }

        [TestCategory("CreatePhoneCallForNewLeadTask")]
        [TestCategory("LeadPlugin")]
        [TestMethod]
        public void CreatePhoneCallForNewLeadPassBothPhones()
        {
            var lead = _testDataService.GetRepository<ILeadTestDataRepo>().GetPhoneCall();
            lead.FirstName = DateTime.Now.ToShortDateString() + DateTime.Now.Date.ToShortTimeString();
            lead.Id = CrmTestDataService.CreateTestEntity(lead);

            var partyAndPointer = CrmDataServiceAf.GetRepository<IActivityPointerCrmRepo>().GetAcPartyAndPointer(lead.Id);


            if (partyAndPointer.pointer?.ActivityId != null)
                CrmTestDataService.AddTestEntityToDelete(new EntityReference(partyAndPointer.pointer.ActivityTypeCode, partyAndPointer.pointer.ActivityId.Value));

            Assert.IsNotNull(partyAndPointer.pointer?.ActivityId);

            var phoneCallFromCrm = CrmDataServiceAf.GetEntities<PhoneCall>(o => o.ActivityId == partyAndPointer.pointer.ActivityId,
                o => new PhoneCall
                {
                    PhoneNumber = o.PhoneNumber
                }).FirstOrDefault();

            Assert.AreEqual(phoneCallFromCrm?.PhoneNumber, lead.MobilePhone);
        }

        [TestCategory("CreatePhoneCallForNewLeadTask")]
        [TestCategory("LeadPlugin")]
        [TestMethod]
        public void CreatePhoneCallForNewLeadPassBusinessPhone()
        {
            var lead = _testDataService.GetRepository<ILeadTestDataRepo>().GetPhoneCall();
            lead.FirstName = DateTime.Now.ToShortDateString()+DateTime.Now.Date.ToShortTimeString();

            lead.Attributes.Remove(nameof(lead.Telephone1).ToLower());

            lead.Id = CrmTestDataService.CreateTestEntity(lead);

            var partyAndPointer = CrmDataServiceAf.GetRepository<IActivityPointerCrmRepo>().GetAcPartyAndPointer(lead.Id);

            if (partyAndPointer.pointer?.ActivityId != null)
                CrmTestDataService.AddTestEntityToDelete(new EntityReference(partyAndPointer.pointer.ActivityTypeCode, partyAndPointer.pointer.ActivityId.Value));

            Assert.IsNotNull(partyAndPointer.pointer?.ActivityId);

            var phoneCallFromCrm = CrmDataServiceAf.GetEntities<PhoneCall>(o => o.ActivityId == partyAndPointer.pointer.ActivityId,
                o => new PhoneCall
                {
                    PhoneNumber = o.PhoneNumber
                }).FirstOrDefault();

            Assert.AreEqual(phoneCallFromCrm?.PhoneNumber, lead.MobilePhone);
        }

        [TestCategory("CreatePhoneCallForNewLeadTask")]
        [TestCategory("LeadPlugin")]
        [TestMethod]
        public void CreatePhoneCallForNewLeadPassTelephone1()
        {
            var lead = _testDataService.GetRepository<ILeadTestDataRepo>().GetPhoneCall();
            lead.FirstName = DateTime.Now.ToShortDateString() + DateTime.Now.Date.ToShortTimeString();
            lead.Attributes.Remove(nameof(lead.MobilePhone).ToLower());

            lead.Id = CrmTestDataService.CreateTestEntity(lead);

            var partyAndPointer = CrmDataServiceAf.GetRepository<IActivityPointerCrmRepo>().GetAcPartyAndPointer(lead.Id);

            if (partyAndPointer.pointer?.ActivityId != null)
                CrmTestDataService.AddTestEntityToDelete(new EntityReference(partyAndPointer.pointer.ActivityTypeCode, partyAndPointer.pointer.ActivityId.Value));

            Assert.IsNotNull(partyAndPointer.pointer?.ActivityId);

            var phoneCallFromCrm = CrmDataServiceAf.GetEntities<PhoneCall>(o => o.ActivityId == partyAndPointer.pointer.ActivityId,
                o => new PhoneCall
                {
                    PhoneNumber = o.PhoneNumber
                }).FirstOrDefault();

            Assert.AreEqual(phoneCallFromCrm?.PhoneNumber, lead.Telephone1);
        }

        [TestCategory("CreatePhoneCallForNewLeadTask")]
        [TestCategory("LeadPlugin")]
        [TestMethod]
        public void CreatePhoneCallForNewLeadPassNoPhone()
        {
            var lead = _testDataService.GetRepository<ILeadTestDataRepo>().GetPhoneCall();
            lead.FirstName = DateTime.Now.ToShortDateString() + DateTime.Now.Date.ToShortTimeString();
            lead.Attributes.Remove(nameof(lead.MobilePhone).ToLower());
            lead.Attributes.Remove(nameof(lead.Telephone1).ToLower());

            lead.Id = CrmTestDataService.CreateTestEntity(lead);

            var partyAndPointer = CrmDataServiceAf.GetRepository<IActivityPointerCrmRepo>().GetAcPartyAndPointer(lead.Id);

            if (partyAndPointer.pointer?.ActivityId != null)
                CrmTestDataService.AddTestEntityToDelete(new EntityReference(partyAndPointer.pointer.ActivityTypeCode, partyAndPointer.pointer.ActivityId.Value));

            Assert.IsNull(partyAndPointer.pointer?.ActivityId);
        }

        [TestCategory("CreatePhoneCallForNewLeadTask")]
        [TestCategory("LeadPlugin")]
        [TestMethod]
        public void CreatePhoneCallForNewLeadPassNoFullName()
        {
            var lead = _testDataService.GetRepository<ILeadTestDataRepo>().GetPhoneCall();
            lead.FirstName = DateTime.Now.ToShortDateString() + DateTime.Now.Date.ToShortTimeString();
            lead.Attributes.Remove(nameof(lead.FirstName).ToLower());
            lead.Attributes.Remove(nameof(lead.LastName).ToLower());

            lead.Id = CrmTestDataService.CreateTestEntity(lead);

            var partyAndPointer = CrmDataServiceAf.GetRepository<IActivityPointerCrmRepo>().GetAcPartyAndPointer(lead.Id);

            Assert.IsNull(partyAndPointer.pointer?.ActivityId);
        }
    }
}