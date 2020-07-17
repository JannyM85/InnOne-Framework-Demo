using InnOne.CrmFramework.Demo.Tasks.Lead;

namespace InnOne.CrmFramework.Demo.Plugins
{
    //Pre
    [CrmPluginRegistration("Create",
        "lead", StageEnum.PreOperation, ExecutionModeEnum.Synchronous,
        "", "PreCreateLead", 1,
        IsolationModeEnum.Sandbox
    )]
    [CrmPluginRegistration("Update",
        "lead", StageEnum.PreOperation, ExecutionModeEnum.Synchronous,
        "", "PreUpdateLead", 1,
        IsolationModeEnum.Sandbox
    )]
    //Post
    [CrmPluginRegistration("Create",
    "lead", StageEnum.PostOperation, ExecutionModeEnum.Synchronous,
    "", "PostCreateLead", 1,
    IsolationModeEnum.Sandbox
    , Id = "e0813ed1-747a-ea11-a843-000d3a494fff"
    )]
    [CrmPluginRegistration("Update",
    "lead", StageEnum.PostOperation, ExecutionModeEnum.Synchronous,
    "", "PostUpdateLead", 1,
    IsolationModeEnum.Sandbox
    , Image1Type = ImageTypeEnum.PostImage
    , Image1Name = "image"
    , Image1Attributes = "mobilephone,telephone1,firstname,lastname,companyname,fullname"
    , Id = "e2813ed1-747a-ea11-a843-000d3a494fff"
    )]
    public class LeadPlugin : PluginBase
    {
        public LeadPlugin(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            //pre
            RegisterEvents(Innone.CrmFramework.CrmFramework.Core.Enums.PluginStage.Preoperation, new[] { "Create", "Update" }, "lead", typeof(ExceptionTask));
            //post
            RegisterEvents(Innone.CrmFramework.CrmFramework.Core.Enums.PluginStage.Postoperation, new[] { "Create", "Update" }, "lead", typeof(ScoreLeadTask));
            RegisterEvents(Innone.CrmFramework.CrmFramework.Core.Enums.PluginStage.Postoperation, new[] { "Create", "Update" }, "lead", typeof(CreatePhoneCallForNewLeadTask));
        }
    }
}
