using System;
using System.Collections.Generic;
using static AppLogic.Card;

namespace AppLogic
{
    #region enum wynik operacji
    /// <summary>
    /// Wynik operacji bankowej
    /// </summary>
    public enum BankActionResult
    {
        NULL = -1,
        SUCCESS = 0,
        REJECTED_CANT_ADD_CARD = 1,
        REJECTED_NO_SUCH_USER = 2,
        REJECTED_INSUFFICIENT_ACCOUNT_BALANCE = 3,
        TRANSACTION_REJECTED = 4
    }
    #endregion

    public class Bank
    {
        #region konstruktory
        /// <summary>
        /// Tworzy obiekt banku o podanej nazwie
        /// </summary>
        /// <param name="name">Nazwa banku</param>
        internal Bank(string name="UNKNOWN")
        {
            Name = name;
            Id = counter++;
            IsActive = true;
            Cards = new List<Card>();
        }

        /// <summary>
        /// Tworzy obiekt banku o podanych parametrach
        /// </summary>
        /// <param name="name">Nazwa banku</param>
        /// <param name="id">Id banku</param>
        /// <param name="isActive">Stan banku (czy jest aktywny)</param>
        internal Bank(string name, int id, bool isActive)
        {
            if (name == null)
                Name = "UNKNOWN";
            else
                Name = name;
            Id = id;
            if (counter <= id)
                counter = id + 1;
            IsActive = isActive;
            Cards = new List<Card>();
        }
        #endregion
        
        #region pola i właściwości
        private static int counter = 0;
        internal string Name { get; set; }
        internal bool IsActive{get; set;}
        internal List<Card> Cards { get; }
        internal int Id { get; }
        #endregion

        #region metody
        #region transakcje
        /// <summary>
        /// Autoryzuje płatność kartą na podaną kwotę
        /// </summary>
        /// <param name="cardNumber">Numer karty</param>
        /// <param name="amount">Kwota transakcji</param>
        /// <returns></returns>
        internal BankActionResult Authorize(string cardNumber, decimal amount)
        {
            foreach (Card c in Cards)
            {
                if (c.Number == cardNumber && c.Authorize(amount) == BankActionResult.SUCCESS)
                    return BankActionResult.SUCCESS;
                else if (c.Number == cardNumber) return BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE;
            }
            return BankActionResult.REJECTED_NO_SUCH_USER;
        }

        /// <summary>
        /// Dokonuje transakcji z podanej karty o podanej kwocie. Podanie kwoty z minusem powoduje zabranie kwoty z karty, a z plusem dodanie kwoty na kartę
        /// </summary>
        /// <param name="cardNumber">Karta do wykonania transakcji</param>
        /// <param name="amount">Kwota transakcji</param>
        /// <returns>Zwraca rezultat wykonania transakcji</returns>
        internal void MakeTransaction(string cardNumber, decimal amount)
        {
            foreach (var card in Cards)
            {
                if (card.Number == cardNumber)
                {
                    card.MakeTransaction(amount);
                    break;
                }
            }
        }
        #endregion

        #region tworzenie kart
        /// <summary>
        /// Funkcja generująca 14-cyfrowy numer karty bankowej
        /// </summary>
        /// <param name="type">typ karty, której numer generujemy</param>
        /// <returns>numer karty</returns>
        private string GenerateCardNumber(CardType type)
        {
            string number = (9999 - Id).ToString();

            while (number.Length < 4)
                number = "0" + number;
            switch (type)
            {
                case CardType.CreditCard:
                    number += "01";
                    break;
                case CardType.DebitCard:
                    number += "02";
                    break;
                case CardType.ATMCard:
                    number += "03";
                    break;
            }
            string hash = (Math.Abs(DateTime.Now.GetHashCode()) % 99979933).ToString();
            while (hash.Length < 8)
                hash = "0" + hash;
            number += hash;
            int checkSum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                checkSum += ((int)number[i] * 13 + 577) % 277;
            }
            checkSum %= 9109;
            hash = checkSum.ToString();
            while (hash.Length < 4)
                hash = "0" + hash;
            return number;
        }

        /// <summary>
        /// Dodaj nowa/istniejaca karte do banku
        /// </summary>
        /// <param name="owner">Użytkownik żądający dodania</param>
        /// <param name="type">Typ dodawanej karty</param>
        /// <returns>
        /// Obiekt utworzonej karty
        /// </returns>
        internal Card AddCard(Client owner, CardType type)
        {
            if (owner == null)
                throw new NullUserException("Nie podano użytkownika");
            string number = GenerateCardNumber(type);
            Card card = null;
            switch (type)
            {
                case CardType.CreditCard:
                    card = new CreditCard(number, owner);
                    break;
                case CardType.DebitCard:
                    card = new DebitCard(number, owner);
                    break;
                case CardType.ATMCard:
                    card = new ATMCard(number, owner);
                    break;
            }
            Cards.Add(card);
            return card;
        }

        /// <summary>
        /// Dodaje kartę do listy banku
        /// </summary>
        /// <param name="card">Obiekt dodawanej karty</param>
        internal void AddCard(Card card)
        {
            if (card == null)
                throw new NoSuchCardException("Probowano dodac pusta karte", "");
            Cards.Add(card);
        }
        #endregion

        #region usuwanie kart i klientów
        /// <summary>
        /// Usuwa kartę z systemu (tzw. soft delete)
        /// </summary>
        /// <param name="number">Numer usuwanej karty</param>
        internal void DeleteCard(string number)
        {
            bool removed = false;
            foreach (var card in Cards)
            {
                if (number == card.Number && card.Balance < 0)
                    throw new NotEmptyAccountException("Na karcie znajduje się debet. Nie można usunąć!!!", card.Balance, card.Number);
                else if (number == card.Number && card.Balance > 0)
                    throw new NotEmptyAccountException("Na karcie znajdują się jeszcze niewykorzystane środki. Nie można usunąć!!!", card.Balance, card.Number);
                else if (number == card.Number)
                {
                    card.IsActive = false;
                    removed = true;
                }
            }
            if (!removed)
                throw new NoSuchCardException("Nie znaleziono karty o podanym numerze", number);
        }

        /// <summary>
        /// Usuwa z systemu klienta i powiązane z nim karty (tzw. soft delete)
        /// </summary>
        /// <param name="client">Obiekt usuwanego klienta</param>
        internal void DeleteClient(Client client)
        {
            foreach (var card in Cards)
            {
                if (card.Owner.Equals(client) && card.Balance < 0)
                    throw new NotEmptyAccountException("Na karcie znajduje się debet. Nie można usunąć!!!", card.Balance, card.Number);
                else if (card.Owner.Equals(client) && card.Balance > 0)
                    throw new NotEmptyAccountException("Na karcie znajdują się jeszcze niewykorzystane środki. Nie można usunąć!!!", card.Balance, card.Number);
                else if (card.Owner.Equals(client))
                {
                    card.IsActive = false;
                }
            }
        }
        #endregion
        #endregion
    }
}
