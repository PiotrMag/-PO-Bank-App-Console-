using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLogic;

namespace AppLogicTests
{
    [TestClass]
    public class ServiceCompanyTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var company = new ServiceCompany("PHU Woźniak", "9661233221");
            Assert.AreEqual("PHU Woźniak", company.Name);
            Assert.AreEqual("9661233221", company.Number);
        }
    }
}
