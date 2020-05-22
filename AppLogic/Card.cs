using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class InsufficientCardBalance : Exception 
    {
        public string CardNumber { get; }
        public double Amount { get; }
        public InsufficientCardBalance(string message) : base(message) { }
    }

    abstract public class Card
    {
        public Client Owner { get; }
        /// <summary>
        /// Numer karty płatniczej
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// tworzy nową kartę płatniczą o podanym numerze
        /// </summary>
        /// <param name="number">numer karty</param>
        public Card(string number, Client owner)
        {
            Number = number;
            Owner = owner;
        }

        abstract public void MakeTransaction(double amount);
    }
}
