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
            var card = new ATMCard("1234567890", client,true, 1250.12m);
            card.MakeTransaction(1200m);
            Assert.AreEqual(card.Balance, 2450.12m);
        }

        [TestMethod]
        public void ATMCardTransactionRejectedTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new ATMCard("1234567890", client, true, 1250.12m);
            try
            {
                card.MakeTransaction(-1500m);
            }
            catch(InsufficientCardBalanceException)
            {
                Assert.AreEqual(card.Balance, 1250.12m);
            }
            Assert.AreEqual(card.Balance, 1250.12m);
        }
        [TestMethod]
        public void ATMCardAuthorizeRejectedTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new ATMCard("1234567890", client, true, 1250.12m);
            Assert.AreEqual(card.Authorize(-1500m), BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE);
        }
        [TestMethod]
        public void ATMCardAuthorizeSuccessfulTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new ATMCard("1234567890", client, true, 1250.12m);
            Assert.AreEqual(card.Authorize(1500m), BankActionResult.SUCCESS);
        }
    }
}
