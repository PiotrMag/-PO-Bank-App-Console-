using System;
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
        /// Probuje autoryzowac karte
        /// </summary>
        /// <returns>
        /// BankActionResult, ktory mowi o tym, czy autoryzacja sie powiodla, czy nie
        /// </returns>
        public BankActionResult Authorize ()
        {
            return BankActionResult.NULL;
        }

        /// <summary>
        /// Dokonuje transakcji z podanej karty o podanej kwocie. Podanie kwoty z minusem powoduje zabranie kwoty z karty, a z plusem dodanie kwoty na kartę
        /// </summary>
        /// <param name="card">Karta do wykonania transakcji</param>
        /// <param name="amount">Kwota do dodania/zabrania</param>
        /// <returns>Zwraca rezultat wykonania transakcji</returns>
        public BankActionResult MakeTransaction(Card card, double amount)
        {
            return BankActionResult.NULL;
        }

        #region obsługa kart (dodawanie/usuwanie)
        /// <summary>
        /// Dodaj nowa/istniejaca karte do banku
        /// </summary>
        /// <param name="card">Karta do dodania</param>
        /// <returns>
        /// BankActionResult, ktory mowi o tym, czy akcja sie powiodla, czy nie
        /// </returns>
        public BankActionResult AddCard(Card card)
        {
            return BankActionResult.NULL;
        }

        /// <summary>
        /// Usuwa kartę z systemu (tzw. soft delete)
        /// </summary>
        /// <param name="number">Numer usuwanej karty</param>
        /// <returns>
        /// BankActionResult, ktory mowi o tym, czy akcja sie powiodla, czy nie
        /// </returns>
        public BankActionResult DeleteCard(string number)
        {
            return BankActionResult.NULL;
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
        /// <returns></returns>
        public BankActionResult DeleteClient(Client client)
        {
            return BankActionResult.NULL;
        }
        #endregion
    }
}
