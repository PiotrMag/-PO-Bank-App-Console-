using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLogic;

namespace AppLogicTests
{
    [TestClass]
    public class ArchiveRecordTests
    {
        [TestMethod]
        public void TestNulledArgumentsInConstructor()
        {
            ArchiveRecord record = new ArchiveRecord(null, null, null, null, null, null, null, null, null, null, null, null, null, null, 0, 0);

            Assert.AreEqual(record.FromName, "NULL");
            Assert.AreEqual(record.FromID, "NULL");
            Assert.AreEqual(record.FromType, "NULL");
            Assert.AreEqual(record.FromCardID, "NULL");
            Assert.AreEqual(record.FromCardType, "NULL");
            Assert.AreEqual(record.FromBankName, "NULL");
            Assert.AreEqual(record.FromBankID, "NULL");
            Assert.AreEqual(record.ToName, "NULL");
            Assert.AreEqual(record.ToID, "NULL");
            Assert.AreEqual(record.ToType, "NULL");
            Assert.AreEqual(record.ToCardID, "NULL");
            Assert.AreEqual(record.ToCardType, "NULL");
            Assert.AreEqual(record.ToBankName, "NULL");
            Assert.AreEqual(record.ToBankID, "NULL");
        }
    }
}
