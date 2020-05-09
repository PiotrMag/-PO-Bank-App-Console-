﻿using System;
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
        internal CreditCard(string number) : base(number)
        {
        }
    }
}