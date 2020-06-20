using System;

namespace AppLogic
{
    internal class ATMCard : Card
    {
        #region konstruktory
        /// <summary>
        /// Tworzy nową kartę bankomatową o podanych parametrach
        /// </summary>
        /// <param name="number">Numer karty</param>
        /// <param name="owner">Właściciel</param>
        /// <param name="isActive">Stan karty(czy jest aktywna)</param>
        /// <param name="balance">Ilość środków na koncie</param>
        internal ATMCard(string number, Client owner, bool isActive, decimal balance) : base(number, owner, isActive, balance)
        {
        }

        /// <summary>
        /// Tworzy nową kartę bankomatową o podanych parametrach
        /// </summary>
        /// <param name="number">Numer karty</param>
        /// <param name="owner">Właściciel</param>
        internal ATMCard(string number, Client owner) : base(number, owner, true, 0)
        {
        }
        #endregion

        #region metody
        /// <summary>
        /// Dodaje do środków na karcie kwotę pobraną jako parametr
        /// </summary>
        /// <param name="amount">Kwota transakcji</param>
        /// <exception cref="InsufficientCardBalanceException"/>
        internal override void MakeTransaction(decimal amount)
        {
            if (balance + amount < 0)
                throw new InsufficientCardBalanceException("Próbowano pobrać większą kwotę niż to możliwe", this.Number, amount);
            balance += amount;
            Math.Round(balance, 2);
        }

        /// <summary>
        /// Autoryzuje transakcję na danej karcie na podaną kwotę
        /// </summary>
        /// <param name="amount">Kwota transakcji</param>
        /// <returns>Wynik autoryzacji</returns>
        internal override BankActionResult Authorize(decimal amount)
        {
            if (balance + amount < 0)
                return BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE;
            return BankActionResult.SUCCESS;
        }
        #endregion
    }
}
