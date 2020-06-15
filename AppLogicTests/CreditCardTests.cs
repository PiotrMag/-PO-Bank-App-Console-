using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLogic;

namespace AppLogicTests
{
    [TestClass]
    public class CreditCardTests
    {
        [TestMethod]
        public void CreditCardSuccessfulTransactionTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new CreditCard("1234567890", client, true, 1250.12m);
            card.MakeTransaction(1200m);
            Assert.AreEqual(card.Balance, 2450.12m);
        }
        [TestMethod]
        public void CreditCardDebitAfterTransactionTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new CreditCard("1234567890", client, true, 1250.12m);
            card.MakeTransaction(-1400m);
            Assert.AreEqual(-149.88m, card.Balance);
        }

        [TestMethod]
        public void CreditCardTransactionRejectedTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new CreditCard("1234567890", client, true, 1250.12m);
            try
            {
                card.MakeTransaction(-3500m);
            }
            catch (InsufficientCardBalanceException)
            {
                Assert.AreEqual(card.Balance, 1250.12m);
            }
            Assert.AreEqual(card.Balance, 1250.12m);
        }
        [TestMethod]
        public void CreditCardAuthorizeRejectedTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new CreditCard("1234567890", client, true, 1250.12m);
            Assert.AreEqual(card.Authorize(-3500), BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE);
        }
        [TestMethod]
        public void CreditCardAuthorizeSuccessfulTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new CreditCard("1234567890", client, true, 1250.12m);
            Assert.AreEqual(card.Authorize(1500), BankActionResult.SUCCESS);
        }
    }
}
