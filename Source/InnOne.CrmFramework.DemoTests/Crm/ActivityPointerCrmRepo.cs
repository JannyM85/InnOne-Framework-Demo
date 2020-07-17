extern alias Demo;
using System;
using System.Linq;
using Demo::InnOne.CrmFramework.Demo;
using Innone.CrmFramework.Autofac.CrmDataService;
using Innone.CrmFramework.CrmDataService;
using InnOne.CrmFramework.DemoTests.Crm.Interfaces;

namespace InnOne.CrmFramework.DemoTests.Crm
{
    public class ActivityPointerCrmRepo: CrmDataRepositoryAf, IActivityPointerCrmRepo
    {
        public ActivityPointerCrmRepo(ICrmDataServiceAf crmDataService) : base(crmDataService)
        {
        }

        public (ActivityPointer pointer, ActivityParty party) GetAcPartyAndPointer(Guid entityId)
        {
            using (var svc = new ServiceContext(CrmDataService.OrganizationService))
            {
                var ac = svc.ActivityPointerSet
                    .Join(svc.ActivityPartySet, pointer => pointer.ActivityId, party => party.ActivityId.Id, (pointer, party) => new { pointer, party })
                    .Where(o => o.party.PartyId.Id == entityId)
                    .Select(o => new { o.pointer, o.party })
                    .FirstOrDefault();

                return (ac?.pointer, ac?.party);
            }
        }
    }
}