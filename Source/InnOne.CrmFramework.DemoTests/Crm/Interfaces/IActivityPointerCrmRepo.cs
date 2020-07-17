extern alias Demo;
using System;
using Demo::InnOne.CrmFramework.Demo;
using Innone.CrmFramework.CrmDataService.Interfaces;

namespace InnOne.CrmFramework.DemoTests.Crm.Interfaces
{
    public interface IActivityPointerCrmRepo: ICrmDataRepository
    {
        (ActivityPointer pointer, ActivityParty party) GetAcPartyAndPointer(Guid entityId);
    }
}