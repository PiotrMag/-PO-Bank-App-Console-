using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLogic;

namespace AppLogicTests
{
    [TestClass]
    public class DebitCardTests
    {
        [TestMethod]
        public void DebitCardSuccessfulTransactionTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new DebitCard("1234567890", client, true, 1250.12);
            card.MakeTransaction(1200);
            Assert.AreEqual(card.Balance, 2450.12);
        }

        [TestMethod]
        public void DebitCardTransactionRejectedTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new DebitCard("1234567890", client, true, 1250.12);
            try
            {
                card.MakeTransaction(-3500);
            }
            catch (InsufficientCardBalanceException)
            {
                Assert.AreEqual(card.Balance, 1250.12);
            }
            Assert.AreEqual(card.Balance, 1250.12);
        }
        [TestMethod]
        public void DebitCardAuthorizeRejectedTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new DebitCard("1234567890", client, true, 1250.12);
            Assert.AreEqual(card.Authorize(-3500), BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE);
        }
        [TestMethod]
        public void DebitCardAuthorizeSuccessfulTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new DebitCard("1234567890", client, true, 1250.12);
            Assert.AreEqual(card.Authorize(1500), BankActionResult.SUCCESS);
        }
    }
}
