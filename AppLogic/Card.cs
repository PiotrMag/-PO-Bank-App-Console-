using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    abstract public class Card
    {
        /// <summary>
        /// tworzy nową kartę płatniczą o podanym numerze
        /// </summary>
        /// <param name="number">numer karty</param>
        public Card(string number)
        {
            Number = number;
        }
        /// <summary>
        /// Numer karty płatniczej
        /// </summary>
        public string Number { get; set; }
    }
}
