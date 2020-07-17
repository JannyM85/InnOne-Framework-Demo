using System;
using Innone.CrmFramework.CrmFramework.Core;
using Innone.CrmFramework.CrmFramework.Core.Enums;
using Innone.CrmFramework.CrmFramework.Core.ServiceLocator;

namespace InnOne.CrmFramework.Demo.Tasks.Lead
{
    public class ExceptionTask : TaskBase<Demo.Lead>
    {
        private readonly Demo.Lead postImage;
        public ExceptionTask(IServiceProvider serviceProvider, IPluginServiceLocator pluginServiceLocator, ITaskContext taskContext) : base(serviceProvider, pluginServiceLocator, taskContext)
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

            if (TaskContext.PrimaryEntityName != ContextEntity.LogicalName)
            {
                AddLogMessageLine($"Entity is not {ContextEntity.LogicalName}");
                return false;
            }

            if (TaskContext.PrimaryEntityName != ContextEntity.LogicalName)
            {
                AddLogMessageLine($"Entity is not {ContextEntity.LogicalName}");
                return false;
            }

            if (!ContextEntity.Contains(nameof(ContextEntity.Description).ToLower()))
            {
                AddLogMessageLine($"Context Entity does not contain attribute {nameof(ContextEntity.Description).ToLower()}");
                return false;
            }

            if (ContextEntity.Description != "exception")
            {
                AddLogMessageLine($"Description are not equal to exception");
                return false;
            }

            return true;
        }

        protected override void DoBeforeExecute()
        {

        }

        protected override void DoExecute()
        {
            throw new Exception("Example Plugin Exception Message");
        }

        protected override void DoAfterExecute()
        {
            
        }

       
    }
}