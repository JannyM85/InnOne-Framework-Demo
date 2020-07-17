using InnOne.CrmFramework.Demo.Tasks;

namespace InnOne.CrmFramework.Demo.Plugins
{
    [CrmPluginRegistration("Update", 
    "lead", StageEnum.PreOperation, ExecutionModeEnum.Synchronous,
    "","PreupdateLeadLogEmailAddress", 1, 
    IsolationModeEnum.Sandbox 
    ,Id = "d9813ed1-747a-ea11-a843-000d3a494fff" 
    )]
    [CrmPluginRegistration("Update", 
    "contact", StageEnum.PreOperation, ExecutionModeEnum.Synchronous,
    "","PreupdateContactLogEmailAddress", 1, 
    IsolationModeEnum.Sandbox 
    ,Id = "db813ed1-747a-ea11-a843-000d3a494fff" 
    )]
    [CrmPluginRegistration("Update", 
    "account", StageEnum.PreOperation, ExecutionModeEnum.Synchronous,
    "","PreupdateAccountLogEmailAddress", 1, 
    IsolationModeEnum.Sandbox 
    ,Id = "dd813ed1-747a-ea11-a843-000d3a494fff" 
    )]
    public class LogEmailAddressPlugin : PluginBase
    {
        public LogEmailAddressPlugin(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            RegisterEvents(Innone.CrmFramework.CrmFramework.Core.Enums.PluginStage.Preoperation, new[] { "Create", "Update" },
                new[]
                {
                    Lead.EntityLogicalName,
                    Contact.EntityLogicalName,
                    Account.EntityLogicalName
                }, typeof(LogEmailAddressChangeTask));
        }
    }
}