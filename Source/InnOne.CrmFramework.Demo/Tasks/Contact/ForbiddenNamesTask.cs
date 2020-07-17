using System;
using System.Collections.Generic;
using System.Linq;
using Innone.CrmFramework.CrmFramework.Core;
using Innone.CrmFramework.CrmFramework.Core.Enums;
using Innone.CrmFramework.CrmFramework.Core.ServiceLocator;
using InnOne.CrmFramework.Demo.Services;
using Microsoft.Xrm.Sdk;

namespace InnOne.CrmFramework.Demo.Tasks.Contact
{
    public class ForbiddenNamesTask : TaskBase<Demo.Contact>
    {
        public ForbiddenNamesTask(IServiceProvider serviceProvider, IPluginServiceLocator pluginServiceLocator, ITaskContext taskContext) : base(serviceProvider, pluginServiceLocator, taskContext)
        {
        }

        protected override bool DoValidate()
        {
            if (TaskContext.Mode != PluginMode.Synchronous)
            {
                AddLogMessageLine($"Plugin mode is not {PluginMode.Synchronous}");
                return false;
            }

            if (TaskContext.Stage != PluginStage.Prevalidation)
            {
                AddLogMessageLine($"Plugin stage is not {PluginStage.Prevalidation}");
                return false;
            }

            if (TaskContext.Message != "Update" && TaskContext.Message != "Create")
            {
                AddLogMessageLine($"Plugin message is not Create or Update");
                return false;
            }

            if (TaskContext.PrimaryEntityName != ContextEntity.LogicalName)
            {
                AddLogMessageLine($"Entity is not {ContextEntity.LogicalName}");
                return false;
            }


            var requiredAttributes = new List<string> { "firstname", "lastname" };
            if (!ContextEntity.Attributes.Keys.ToList().Intersect(requiredAttributes).Any())
            {
                AddLogMessageLine($"ContextEntity does not contain any of the attributes: {string.Join(", ", requiredAttributes)}");
                return false;
            }

            return true;
        }

        protected override void DoBeforeExecute()
        {
        }

        protected override void DoExecute()
        {
            var forbiddenWords = PluginServiceLocator.GetService<CustomerForbiddenNameService>().GetForbiddenNames();

            if (ContextEntity.Contains(nameof(ContextEntity.FirstName).ToLower()) &&
               forbiddenWords.FindIndex(x => x.Equals(ContextEntity.FirstName, StringComparison.InvariantCultureIgnoreCase)) != -1)
            {
                var msg =  "First name is forbidden word, please write correct your first name";
                AddLogMessageLine(msg);
                //this exception  will not be logged
                throw new CrmNotLoggingException(msg);
            }

            if (ContextEntity.Contains(nameof(ContextEntity.LastName).ToLower()) &&
                forbiddenWords.FindIndex(x => x.Equals(ContextEntity.LastName, StringComparison.InvariantCultureIgnoreCase)) != -1)
            {
                var msg = "Last name is forbidden word, please write correct your last name";
                AddLogMessageLine(msg);
                
                //this exception  will be logged
                throw new InvalidPluginExecutionException(msg);
            }
        }

        protected override void DoAfterExecute()
        {
        }
    }
}