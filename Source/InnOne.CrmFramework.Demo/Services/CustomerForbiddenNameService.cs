using System.Collections.Generic;
using Innone.CrmFramework.CrmFramework.Setting;
using Newtonsoft.Json;

namespace InnOne.CrmFramework.Demo.Services
{
    public class CustomerForbiddenNameService
    {
        private readonly ISettingService _settingService;
        public CustomerForbiddenNameService(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        public List<string> GetForbiddenNames()
        {
            var listJson = _settingService.GetJsonValue(SettingKeys.ForbiddenWords);

           var list = JsonConvert.DeserializeObject<List<string>>(listJson);

           return list;
        }
    }
}