using System;
using System.Collections.Generic;
using System.Linq;
using Innone.CrmFramework.CrmFramework.Core;
using Innone.CrmFramework.CrmFramework.Core.Enums;
using Innone.CrmFramework.CrmFramework.Core.ServiceLocator;
using Microsoft.Xrm.Sdk;

namespace InnOne.CrmFramework.Demo.Tasks
{
    public class LogEmailAddressChangeTask : TaskBase<Entity>
    {
        private readonly Demo.Lead postImage;
        public LogEmailAddressChangeTask(IServiceProvider serviceProvider, IPluginServiceLocator pluginServiceLocator, ITaskContext taskContext) : base(serviceProvider, pluginServiceLocator, taskContext)
        {
        }

        protected override bool DoValidate()
        {
            if (TaskContext.Mode != PluginMode.Synchronous)
            {
                AddLogMessageLine($"Plugin mode is not {PluginMode.Synchronous}");
                return false;
            }

            if (TaskContext.Stage != PluginStage.Preoperation)
            {
                AddLogMessageLine($"Plugin stage is not {PluginStage.Preoperation}");
                return false;
            }

            if (TaskContext.Message != "Update" && TaskContext.Message != "Create")
            {
                AddLogMessageLine($"Plugin message is not Create or Update");
                return false;
            }


            var emailAddressAttNames = new List<string> { "emailaddress1", "emailaddress2", "emailaddress3" };
            if (!ContextEntity.Attributes.Keys.ToList().Intersect(emailAddressAttNames).Any())
            {
                AddLogMessageLine($"ContextEntity does not contain any of the attributes: {string.Join(", ", emailAddressAttNames)}");
                return false;
            }

            return true;
        }

        protected override void DoBeforeExecute()
        {

        }

        protected override void DoExecute()
        {
            AddLogMessageLine("Do some email address logging...");
        }

        protected override void DoAfterExecute()
        {

        }


    }
}