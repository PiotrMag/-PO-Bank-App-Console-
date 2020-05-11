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
    }
}
