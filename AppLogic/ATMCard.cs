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
        public ATMCard(string number) : base(number)
        {
        }
    }
}
