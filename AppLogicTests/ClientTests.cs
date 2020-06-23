using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLogic;

namespace AppLogicTests
{
    [TestClass]
    public class ClientTests
    {
        #region NaturalPerson
        [TestMethod]
        public void NaturalPersonAreEqualTest()
        {
            var client = new NaturalPerson("Adam Nowak", "00211934518");
            var client2 = new NaturalPerson("Adam Nowak", "00211934518");
            Assert.AreEqual(client, client2);
        }
        [TestMethod]
        public void NaturalPersonAreNotEqualDifferentNamesTest()
        {
            var client = new NaturalPerson("Adam Nowak", "00211934518");
            var client2 = new NaturalPerson("Jan Kowalski", "00211934518");
            Assert.AreNotEqual(client, client2);
        }
        [TestMethod]
        public void NaturalPersonAreNotEqualDifferentPESELTest()
        {
            var client = new NaturalPerson("Adam Nowak", "00211934518");
            var client2 = new NaturalPerson("Adam Nowak", "99011934518");
            Assert.AreNotEqual(client, client2);
        }
        #endregion

        #region ServiceCompany
        [TestMethod]
        public void ServiceCompanyAreEqualTest()
        {
            var company = new ServiceCompany("PHU Woźniak", "9661233221");
            var company2 = new ServiceCompany("PHU Woźniak", "9661233221");
            Assert.AreEqual(company, company2);
        }
        [TestMethod]
        public void ServiceCompanyAreNotEqualDifferentNamesTest()
        {
            var company = new ServiceCompany("PHU Mur-Beton", "9661233221");
            var company2 = new ServiceCompany("PHU Woźniak", "9661233221");
            Assert.AreNotEqual(company, company2);
        }
        [TestMethod]
        public void ServiceCompanyAreNotEqualDifferentNumbersTest()
        {
            var company = new ServiceCompany("PHU Woźniak", "9661233221");
            var company2 = new ServiceCompany("PHU Woźniak", "9645332210");
            Assert.AreNotEqual(company, company2);
        }
        #endregion

        #region TransportCompany
        [TestMethod]
        public void TransportCompanyAreEqualTest()
        {
            var company = new TransportCompany("PeKaeS", "9661233221");
            var company2 = new TransportCompany("PeKaeS", "9661233221");
            Assert.AreEqual(company, company2);
        }
        [TestMethod]
        public void TransportCompanyAreNotEqualDifferentNamesTest()
        {
            var company = new TransportCompany("DPD", "9661233221");
            var company2 = new TransportCompany("PeKaeS", "9661233221");
            Assert.AreNotEqual(company, company2);
        }
        [TestMethod]
        public void TransportCompanyAreNotEqualDifferentNumbersTest()
        {
            var company = new TransportCompany("PeKaeS", "9661233221");
            var company2 = new TransportCompany("PeKaeS", "9645332210");
            Assert.AreNotEqual(company, company2);
        }
        #endregion

        #region Shop
        [TestMethod]
        public void ShopsAreEqualTest()
        {
            var company = new Shop("Jeronimo Martins S.A.", "9661233221");
            var company2 = new Shop("Jeronimo Martins S.A.", "9661233221");
            Assert.AreEqual(company, company2);
        }
        [TestMethod]
        public void ShopsAreNotEqualDifferentNamesTest()
        {
            var company = new Shop("Transgourmet Polska Sp. z o.o.", "9661233221");
            var company2 = new Shop("Jeronimo Martins S.A.", "9661233221");
            Assert.AreNotEqual(company, company2);
        }
        [TestMethod]
        public void ShopsAreNotEqualDifferentNumbersTest()
        {
            var company = new Shop("Jeronimo Martins S.A.", "9661233221");
            var company2 = new Shop("Jeronimo Martins S.A.", "9645332210");
            Assert.AreNotEqual(company, company2);
        }
        #endregion
    }
}