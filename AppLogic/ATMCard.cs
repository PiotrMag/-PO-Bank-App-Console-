using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class ATMCard : Card
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
            this.balance += amount;
        }
        public override BankActionResult Authorize(double amount)
        {
            return BankActionResult.SUCCESS;
        }
    }
}
