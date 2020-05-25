﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public enum BankActionResult
    {
        NULL = -1,
        SUCCESS = 0,
        REJECTED_CANT_ADD_CARD = 1,
        REJECTED_NO_SUCH_USER = 2,
        REJECTED_INSUFFICIENT_ACCOUNT_BALANCE = 3
    }

    public class Bank
    {
        private static int counter = 0;
        public string Name { get; set; }
        public List<Card> cards { get; }
        public int Id { get; }

        /// <summary>
        /// Konstruktor obiektu Bank
        /// </summary>
        /// <param name="name">Nazwa banku</param>
        public Bank(string name)
        {
            Name = name;
            Id = counter++;
        }

        /// <summary>
        /// Probuje autoryzowac płatność kartą na podaną kwotę
        /// </summary>
        /// <returns>
        /// BankActionResult, ktory mowi o tym, czy autoryzacja sie powiodla, czy nie
        /// </returns>
        public BankActionResult Authorize(string cardNumber, double amount)
        {
            foreach (Card c in cards)
            {
                if (c.Number == cardNumber && c.Authorize(amount)==BankActionResult.SUCCESS)
                    return BankActionResult.SUCCESS;
            }
            return BankActionResult.REJECTED_NO_SUCH_USER;
        }

        /// <summary>
        /// Dokonuje transakcji z podanej karty o podanej kwocie. Podanie kwoty z minusem powoduje zabranie kwoty z karty, a z plusem dodanie kwoty na kartę
        /// </summary>
        /// <param name="cardNumber">Karta do wykonania transakcji</param>
        /// <param name="amount">Kwota do dodania/zabrania</param>
        /// <returns>Zwraca rezultat wykonania transakcji</returns>
        public void MakeTransaction(string cardNumber, double amount)
        {
            foreach (var card in cards)
            {
                if (card.Number == cardNumber)
                {
                    card.MakeTransaction(amount);
                }
            }
        }

        #region obsługa kart (dodawanie/usuwanie)

        /// <summary>
        /// Funkcja generująca 18-cyfrowy numer karty bankowej
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
        public Card AddCard(Client owner, CardType type)
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
            return card;
        }

        /// <summary>
        /// Usuwa kartę z systemu (tzw. soft delete)
        /// </summary>
        /// <param name="number">Numer usuwanej karty</param>
        public void DeleteCard(string number)
        {
            bool removed = false;
            foreach (var card in cards)
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
                throw new NoSuchCardException("Nie znaleziono karty o podanym numerze");
        }
        #endregion

        #region obsługa klientów (dodawanie/usuwanie)
        /// <summary>
        /// Dodaje do systemu nowego klienta
        /// </summary>
        /// <param name="client">Obiekt dodawanego klienta</param>
        /// <returns></returns>
        public BankActionResult AddClient(Client client)
        {
            return BankActionResult.NULL;
        }

        /// <summary>
        /// Usuwa z systemu klienta i powiązane z nim karty (tzw. soft delete)
        /// </summary>
        /// <param name="client">Obiekt usuwanego klienta</param>
        public void DeleteClient(Client client)
        {
            foreach (var card in cards)
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
    }
}
