using Innone.CrmFramework.CrmFramework.Core.Helpers;
using InnOne.CrmFramework.DemoTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InnOne.CrmFramework.DemoTests.Tests
{
    [TestClass()]
    public class CrmHelperTest: CrmTestBase
    {
        [TestMethod]
        public void GetWebResourceTest()
        {
            var ent = CrmHelper.GetWebResourceByName("in_applicationlogo", CrmDataServiceAf.OrganizationService);
            Assert.IsNotNull(ent);
        }
    }
}