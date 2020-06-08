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
        internal DebitCard(string number, Client owner, bool isActive, double balance) : base(number, owner, isActive, balance)
        {
        }

        public override void MakeTransaction(double amount)
        {
            if (this.balance + amount < 0)
                throw new InsufficientCardBalanceException("Kwota transakcji przekracza stan karty debetowej", this.Number, amount);
            this.balance += amount;
        }
        public override BankActionResult Authorize(double amount)
        {
            if (balance + amount < 0)
                return BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE;
            return BankActionResult.SUCCESS;
        }
    }
}
