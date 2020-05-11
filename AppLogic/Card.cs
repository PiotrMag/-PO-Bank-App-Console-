using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    abstract public class Card
    {
        public Client owner;

        /// <summary>
        /// tworzy nową kartę płatniczą o podanym numerze
        /// </summary>
        /// <param name="number">numer karty</param>
        public Card(string number, Client owner)
        {
            Number = number;
            this.owner = owner;
        }
        /// <summary>
        /// Numer karty płatniczej
        /// </summary>
        public string Number { get; set; }
    }
}
