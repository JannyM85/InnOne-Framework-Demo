using Innone.CrmFramework.CrmFramework.Core;
using Innone.CrmFramework.CrmFramework.Setting;
using InnOne.CrmFramework.Demo.Services;

namespace InnOne.CrmFramework.Demo.Plugins
{
    public class PluginBase: PluginExecutorBase
    {
        public PluginBase(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            OnRegisterService += (sender, args) =>
            {
                var settingService = new SettingService(args.AdminOrganizationService);

                var svc = new CustomerForbiddenNameService(settingService);
                args.ServiceLocator.RegisterService(svc);
            };
        }

        public override string GetSolutionVersion()
        {
            return "1.5";
        }
    }
}