using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLogic;

namespace AppLogicTests
{
    [TestClass]
    class ATMCardTests
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
            catch(InsufficientCardBalanceException ex)
            {
                Assert.AreEqual(card.Balance, 1250.12);
            }
            Assert.AreEqual(card.Balance, 1250.12);
        }
    }
}
