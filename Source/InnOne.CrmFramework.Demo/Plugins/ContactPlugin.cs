using InnOne.CrmFramework.Demo.Tasks.Contact;

namespace InnOne.CrmFramework.Demo.Plugins
{
    [CrmPluginRegistration("Create", 
    "contact", StageEnum.PreValidation, ExecutionModeEnum.Synchronous,
    "","PreValCreateContact", 1, 
    IsolationModeEnum.Sandbox 
    ,Id = "d5813ed1-747a-ea11-a843-000d3a494fff" 
    )]
    [CrmPluginRegistration("Update", 
    "contact", StageEnum.PreValidation, ExecutionModeEnum.Synchronous,
    "","PrevalUpdateContact", 1, 
    IsolationModeEnum.Sandbox 
    ,Id = "d7813ed1-747a-ea11-a843-000d3a494fff" 
    )]
    public class ContactPlugin : PluginBase
    {
        public ContactPlugin(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            RegisterEvents(Innone.CrmFramework.CrmFramework.Core.Enums.PluginStage.Prevalidation, new[] { "Create", "Update" }, Contact.EntityLogicalName, typeof(ForbiddenNamesTask));
        }
    }
}