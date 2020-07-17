using System;
using System.Linq;
using Innone.CrmFramework.CrmFramework.Core;
using Innone.CrmFramework.CrmFramework.Core.Enums;
using Innone.CrmFramework.CrmFramework.Core.ServiceLocator;
using Microsoft.Xrm.Sdk;

namespace InnOne.CrmFramework.Demo.Tasks.Lead
{
    public class CreatePhoneCallForNewLeadTask : TaskBase<Demo.Lead>
    {
        private readonly Demo.Lead _postImage;
        private string Subject = null;
        public CreatePhoneCallForNewLeadTask(IServiceProvider serviceProvider, IPluginServiceLocator pluginServiceLocator, ITaskContext taskContext) : base(serviceProvider, pluginServiceLocator, taskContext)
        {
            _postImage = ContextEntity;
            if (TaskContext.Message == "Update")
                _postImage = GetPostImage();
        }

        protected override bool DoValidate()
        {
            if (TaskContext.Mode != PluginMode.Synchronous)
            {
                AddLogMessageLine($"Plugin mode is not {PluginMode.Synchronous}");
                return false;
            }

            if (TaskContext.Stage != PluginStage.Postoperation)
            {
                AddLogMessageLine($"Plugin stage is not {PluginStage.Postoperation}");
                return false;
            }

            if (TaskContext.Message != "Create" && TaskContext.Message != "Update")
            {
                AddLogMessageLine($"Plugin message is not Create or Update");
                return false;
            }

            if (TaskContext.PrimaryEntityName != ContextEntity.LogicalName)
            {
                AddLogMessageLine($"Entity is not {ContextEntity.LogicalName}");
                return false;
            }

            if (string.IsNullOrEmpty(_postImage.Telephone1) && string.IsNullOrEmpty(_postImage.MobilePhone))
            {
                AddLogMessageLine($"Entity does not have Telephone1 or MobilePhone");
                return false;
            }

            if (string.IsNullOrEmpty(_postImage.FullName))
            {
                AddLogMessageLine($"Entity does not have FullName");
                return false;
            }

            Subject = $"New Lead => Call to {_postImage.FullName}, {_postImage.CompanyName}";

            var existPhoneCall = false;
            using (var svc = new ServiceContext(UserOrganizationService))
            {
                existPhoneCall = svc.PhoneCallSet.Where(o => o.Subject == Subject).Select(o => o.ActivityId).FirstOrDefault() != null;
            }

            if (existPhoneCall)
            {
                AddLogMessageLine($"PhoneCall with subject:'{Subject}' already exists");
                return false;
            }

            return true;
        }

        protected override void DoBeforeExecute()
        {

        }

        protected override void DoExecute()
        {
            var dayPeriod = SettingService.GetIntegerValue(SettingKeys.CreatePhoneCallForLeadDaysPeriod);
            AddLogMessageLine($"DayPeriod: {dayPeriod}");

            var phoneCall = new PhoneCall
            {
                Subject = Subject,
                ActualStart = DateTime.Now.AddDays(dayPeriod ?? 7),
                Description = $"Call to {_postImage.FullName} and ask him about next steps.",
                To = new[] { new ActivityParty { PartyId = ContextEntityReference }, },
                From = new[] { new ActivityParty { PartyId = new EntityReference("systemuser", TaskContext.UserId) } },
                PhoneNumber = _postImage.MobilePhone ?? _postImage.Telephone1
            };
            UserOrganizationService.Create(phoneCall);
        }

        protected override void DoAfterExecute()
        {

        }
    }
}