using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    internal class DebitCard : Card
    {
        /// <summary>
        /// Tworzy nową kartę debetową o podanym numerze
        /// </summary>
        /// <param name="number">Numer karty</param>
        internal DebitCard(string number, Client owner, bool isActive, decimal balance) : base(number, owner, isActive, balance)
        {
        }

        internal DebitCard(string number, Client owner) : base(number, owner, true, 0)
        {
        }

        public override void MakeTransaction(decimal amount)
        {
            if (balance + amount < 0)
                throw new InsufficientCardBalanceException("Kwota transakcji przekracza stan karty debetowej", this.Number, amount);
            balance += amount;
            Math.Round(balance, 2);
        }
        public override BankActionResult Authorize(decimal amount)
        {
            if (balance + amount < 0)
                return BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE;
            return BankActionResult.SUCCESS;
        }
    }
}
