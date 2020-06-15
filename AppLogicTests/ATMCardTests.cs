using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLogic;

namespace AppLogicTests
{
    [TestClass]
    public class ATMCardTests
    {
        [TestMethod]
        public void ATMCardSuccessfulTransactionTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new ATMCard("1234567890", client,true, 1250.12);
            card.MakeTransaction(1200);
            Assert.AreEqual(card.Balance, 2450.12);
        }

        [TestMethod]
        public void ATMCardTransactionRejectedTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new ATMCard("1234567890", client, true, 1250.12);
            try
            {
                card.MakeTransaction(-1500);
            }
            catch(InsufficientCardBalanceException)
            {
                Assert.AreEqual(card.Balance, 1250.12);
            }
            Assert.AreEqual(card.Balance, 1250.12);
        }
        [TestMethod]
        public void ATMCardAuthorizeRejectedTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new ATMCard("1234567890", client, true, 1250.12);
            Assert.AreEqual(card.Authorize(-1500), BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE);
        }
        [TestMethod]
        public void ATMCardAuthorizeSuccessfulTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new ATMCard("1234567890", client, true, 1250.12);
            Assert.AreEqual(card.Authorize(1500), BankActionResult.SUCCESS);
        }
    }
}
