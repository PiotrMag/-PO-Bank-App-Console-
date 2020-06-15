using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    internal class ATMCard : Card
    {
        /// <summary>
        /// Tworzy nową kartę bankomatową o podanym numerze
        /// </summary>
        /// <param name="number">Numer karty</param>
        public ATMCard(string number, Client owner, bool isActive, double balance) : base(number, owner, isActive, balance)
        {
        }

        public ATMCard(string number, Client owner) : base(number, owner, true, 0)
        {
        }

        public override void MakeTransaction(double amount)
        {
            if (balance + amount < 0)
                throw new InsufficientCardBalanceException("Próbowano pobrać większą kwotę niż to możliwe", this.Number, amount);
            balance += amount;
            Math.Round(balance, 2);
        }
        public override BankActionResult Authorize(double amount)
        {
            if (balance + amount < 0)
                return BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE;
            return BankActionResult.SUCCESS;
        }
    }
}
