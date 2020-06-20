using System;

namespace AppLogic
{
    class DebitCard : Card
    {
        #region konstruktory
        /// <summary>
        /// Tworzy nową kartę kredytową o podanych parametrach
        /// </summary>
        /// <param name="number">Numer karty</param>
        /// <param name="owner">Właściciel karty</param>
        /// <param name="isActive">Stan karty (czy jest aktywna)</param>
        /// <param name="balance">Ilość środków na karcie</param>
        internal DebitCard(string number, Client owner, bool isActive, decimal balance) : base(number, owner, isActive, balance)
        {
        }

        /// <summary>
        /// Tworzy nową kartę kredytową o podanych parametrach
        /// </summary>
        /// <param name="number">Numer karty</param>
        /// <param name="owner">Właściciel karty</param>
        internal DebitCard(string number, Client owner) : base(number, owner, true, 0)
        {
        }
        #endregion

        #region metody
        /// <summary>
        /// Dokonuje na karcie transakcji na podaną kwotę
        /// </summary>
        /// <param name="amount">Kwota transakcji</param>
        /// <exception cref="InsufficientCardBalanceException"/>
        internal override void MakeTransaction(decimal amount)
        {
            if (balance + amount < 0)
                throw new InsufficientCardBalanceException("Kwota transakcji przekracza stan karty debetowej", this.Number, amount);
            balance += amount;
            Math.Round(balance, 2);
        }

        /// <summary>
        /// autoryzuje transakcję na podaną kwotę
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
