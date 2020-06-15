using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLogic;

namespace AppLogicTests
{
    [TestClass]
    public class ShopTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var company = new ServiceCompany("Jeronimo Martins S.A.", "9861233221");
            Assert.AreEqual("Jeronimo Martins S.A.", company.Name);
            Assert.AreEqual("9861233221", company.Number);
        }
    }
}
