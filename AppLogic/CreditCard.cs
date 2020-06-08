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
        internal CreditCard(string number, Client owner, bool isActive, double balance, double creditLimit=1000) : base(number, owner, isActive, balance)
        {
            this.creditLimit = -creditLimit;
        }

        public double CreditLimit { get { return -1 * (creditLimit); }}

        private double creditLimit;

        public override void MakeTransaction(double amount)
        {
            if(balance + amount < creditLimit)
                throw new InsufficientCardBalanceException("Kwota transakcji przekracza dopuszczalny limit karty kredytowej", Number, amount);
            balance += amount;
        }
        public override BankActionResult Authorize(double amount)
        {
            if (balance + amount < creditLimit)
                return BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE;
            return BankActionResult.SUCCESS;
        }
    }
}
