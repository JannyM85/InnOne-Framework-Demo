extern alias Demo;
using System;
using System.Linq;
using Demo::InnOne.CrmFramework.Demo;
using Innone.CrmFramework.Autofac.CrmDataService;
using Innone.CrmFramework.Autofac.CrmTest;
using Innone.CrmFramework.Autofac.CrmTest.Interfaces;
using Innone.CrmFramework.CrmDataService;
using InnOne.CrmFramework.DemoTests.Crm.Interfaces;
using Microsoft.Xrm.Sdk;

namespace InnOne.CrmFramework.DemoTests.DeleteHandlers
{
    public class LeadDeleteHandler : ICleanUpDeleteHandler
    {
        public string EntityLogicalName { get; set; } = Lead.EntityLogicalName;
        public void DeleteReferences(EntityReference entity, ICrmDataServiceAf service)
        {
            var partyAndPointer = service.GetRepository<IActivityPointerCrmRepo>().GetAcPartyAndPointer(entity.Id);

            if (partyAndPointer.pointer?.ActivityId != null)
                service.OrganizationService.Delete(partyAndPointer.pointer.ActivityTypeCode, partyAndPointer.pointer.ActivityId.Value);
        }

      

    }
}