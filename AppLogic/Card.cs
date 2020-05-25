using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    abstract public class Card
    {
        public Client Owner { get; }
        /// <summary>
        /// Numer karty płatniczej
        /// </summary>
        public string Number { get; set; }
        public bool IsActive { get; set; }
        public double Balance { get { return balance; } }

        protected double balance;

        /// <summary>
        /// tworzy nową kartę płatniczą o podanym numerze
        /// </summary>
        /// <param name="number">numer karty</param>
        public Card(string number, Client owner)
        {
            Number = number;
            Owner = owner;
            balance = 0;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Card))
                return false;
            Card card = (Card)obj;
            if (card.Number == this.Number && card.Owner.Equals(this.Owner))
                return true;
            return false;
        }

        /// <summary>
        /// Abstrakcyjna metoda, której zadaniem jest dokonanie transakcji na kartę
        /// </summary>
        /// <param name="amount">Kwota</param>
        /// <exception cref="InsufficientCardBalanceException">Wyrzuca wyjątek InsufficientCardBalanceException, jeżeli typ karty nie pozwala na przekroczenie 0 na koncie</exception>
        abstract public void MakeTransaction(double amount);
        abstract public BankActionResult Authorize(double amount);
    }
}
