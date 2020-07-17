using System;
using Innone.CrmFramework.CrmFramework.Core;
using Innone.CrmFramework.CrmFramework.Core.Enums;
using Innone.CrmFramework.CrmFramework.Core.ServiceLocator;

namespace InnOne.CrmFramework.Demo.Tasks.Lead
{
    public class ScoreLeadTask : TaskBase<InnOne.CrmFramework.Demo.Lead>
    {
        private readonly Demo.Lead postImage;
        public ScoreLeadTask(IServiceProvider serviceProvider, IPluginServiceLocator pluginServiceLocator, ITaskContext taskContext) : base(serviceProvider, pluginServiceLocator, taskContext)
        {
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

            var mobAttname = nameof(ContextEntity.MobilePhone).ToLower();
            if (!ContextEntity.Contains(mobAttname))
            {
                AddLogMessageLine($"ContextEntity does not contains {mobAttname}");
                return false;
            }
            
            if (string.IsNullOrEmpty(ContextEntity.MobilePhone))
            {
                AddLogMessageLine($"Mobile phone is empty");
                return false;
            }

            return true;
        }

        protected override void DoBeforeExecute()
        {

        }

        protected override void DoExecute()
        {
            AddLogMessageLine("Do some scoring...");
        }

        protected override void DoAfterExecute()
        {
            
        }

       
    }
}