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
    }

    public class Bank
    {
        public String Name { get; set; }
        public List<Card> cards { get; }

        /// <summary>
        /// Konstruktor obiektu Bank
        /// </summary>
        /// <param name="name">Nazwa banku</param>
        public Bank(String name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Probuje autoryzowac karte i dokonac platnosci
        /// </summary>
        /// <returns>
        /// BankActionResult, ktory mowi o tym, czy autoryzacja sie powiodla, czy nie
        /// </returns>
        public BankActionResult Authorize ()
        {
            return BankActionResult.NULL;
        }

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
    }
}
