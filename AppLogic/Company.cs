using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    abstract public class Company : Client
    {
        /// <summary>
        /// Metoda wysyłająca prośbę o dokonanie transakcji do banków obsługujących dane karty
        /// </summary>
        /// <param name="fromCard">Karta, z której ma zostać zabrana kwota</param>
        /// <param name="toCard">Karta, na która ma zostaś wpłacona kwota</param>
        /// <param name="amount">Kwota</param>
        /// <returns>Wynik wykonania transakcji</returns>
        public BankActionResult MakeTransactionRequest(Card fromCard, Card toCard, double amount)
        {
            return BankActionResult.NULL;
        }

        public Company(string name, string NIP) : base(name, NIP)
        {

        }
    }
}
