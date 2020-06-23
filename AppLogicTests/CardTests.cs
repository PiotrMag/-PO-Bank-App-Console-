using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLogic;


namespace AppLogicTests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void CreditCardsAreEqualTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new CreditCard("1234567890", client, true, 1250.12m);
            var card2 = new CreditCard("1234567890", client, true, 1250.12m);
            Assert.AreEqual(card, card2);
        }

        [TestMethod]
        public void CreditCardsAreNotEqualDifferentNumbersTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new CreditCard("1233567890", client, true, 1250.12m);
            var card2 = new CreditCard("1234567890", client, true, 1250.12m);
            Assert.AreNotEqual(card, card2);
        }

        [TestMethod]
        public void CreditCardsAreNotEqualDifferentOwnersTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new CreditCard("1234567890", client, true, 1250.12m);
            var client2 = new NaturalPerson("Adam Nowak", "11397737254");
            var card2 = new CreditCard("1234567890", client2, true, 1250.12m);
            Assert.AreNotEqual(card, card2);
        }

        [TestMethod]
        public void DebitCardsAreEqualTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new DebitCard("1234567890", client, true, 1250.12m);
            var card2 = new DebitCard("1234567890", client, true, 1250.12m);
            Assert.AreEqual(card, card2);
        }

        [TestMethod]
        public void DebitCardsAreNotEqualDifferentNumbersTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new DebitCard("1233567890", client, true, 1250.12m);
            var card2 = new DebitCard("1234567890", client, true, 1250.12m);
            Assert.AreNotEqual(card, card2);
        }

        [TestMethod]
        public void DebitCardsAreNotEqualDifferentOwnersTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new DebitCard("1234567890", client, true, 1250.12m);
            var client2 = new NaturalPerson("Adam Nowak", "11397737254");
            var card2 = new DebitCard("1234567890", client2, true, 1250.12m);
            Assert.AreNotEqual(card, card2);
        }

        [TestMethod]
        public void ATMCardsAreEqualTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new ATMCard("1234567890", client, true, 1250.12m);
            var card2 = new ATMCard("1234567890", client, true, 1250.12m);
            Assert.AreEqual(card, card2);
        }

        [TestMethod]
        public void ATMCardsAreNotEqualDifferentNumbersTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new ATMCard("1233567890", client, true, 1250.12m);
            var card2 = new ATMCard("1234567890", client, true, 1250.12m);
            Assert.AreNotEqual(card, card2);
        }

        [TestMethod]
        public void ATMCardsAreNotEqualDifferentOwnersTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card = new ATMCard("1234567890", client, true, 1250.12m);
            var client2 = new NaturalPerson("Adam Nowak", "11397737254");
            var card2 = new ATMCard("1234567890", client2, true, 1250.12m);
            Assert.AreNotEqual(card, card2);
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            var client = new NaturalPerson("Jan Kowalski", "11312737254");
            var card1 = new ATMCard("1234567890", client, true, 1250.12m);
            var card2 = new CreditCard("1254367890", client, true, 1250.12m);
            var card3 = new DebitCard("1234587690", client, true, 1250.12m);
            Assert.AreEqual(1234567890, card1.GetHashCode());
            Assert.AreEqual(1254367890, card2.GetHashCode());
            Assert.AreEqual(1234587690, card3.GetHashCode());
        }
    }
}
