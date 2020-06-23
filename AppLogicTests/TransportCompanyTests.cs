using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLogic;

namespace AppLogicTests
{
    [TestClass]
    public class TransportCompanyTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var company = new TransportCompany("PeKaeS", "9661233000");
            Assert.AreEqual("PeKaeS", company.Name);
            Assert.AreEqual("9661233000", company.Number);
        }
    }
}
