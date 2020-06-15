using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    internal class CreditCard : Card
    {
        /// <summary>
        /// Tworzy nową kartę kredytową o podanym numerze
        /// </summary>
        /// <param name="number">Numer karty</param>
        internal CreditCard(string number, Client owner, bool isActive, decimal balance, decimal creditLimit =1000) : base(number, owner, isActive, balance)
        {
            this.creditLimit = -creditLimit;
        }

        internal CreditCard(string number, Client owner, decimal creditLimit = 1000) : base(number, owner, true, 0)
        {
            this.creditLimit = -creditLimit;
        }

        public decimal CreditLimit { get { return -1 * (creditLimit); }}

        private decimal creditLimit;

        public override void MakeTransaction(decimal amount)
        {
            if(balance + amount < creditLimit)
                throw new InsufficientCardBalanceException("Kwota transakcji przekracza dopuszczalny limit karty kredytowej", Number, amount);
            balance += amount;
            balance *= 100;
            Math.Round(balance);
            balance /= 100;
        }
        public override BankActionResult Authorize(decimal amount)
        {
            if (balance + amount < creditLimit)
                return BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE;
            return BankActionResult.SUCCESS;
        }
    }
}
