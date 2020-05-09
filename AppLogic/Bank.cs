using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public enum AuthorizationResult
    {
        NULL = -1,
        ACCEPTED = 0,
        REJECTED_NO_SUCH_USER = 1,
    }

    public class Bank
    {
        public String Name { get; set; }
        public Bank(String name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Probuje autoryzowac karte i dokonac platnosci
        /// </summary>
        /// <returns>
        /// Zwraca AuthorizationResult, ktory mowi o tym, czy autoryzacja sie powiodla, czy nie
        /// </returns>
        public AuthorizationResult Authorize ()
        {
            return AuthorizationResult.NULL;
        }
    }
}
