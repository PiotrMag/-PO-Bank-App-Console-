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
        internal DebitCard(string number, Client owner) : base(number, owner)
        {
        }

        public override void MakeTransaction(double amount)
        {
            if (this.balance + amount < 0)
                throw new InsufficientCardBalance("Probowano zabrac z karty debetowej wiecej niz na niej jest!");
            this.balance += amount;
        }
    }
}
