using System;

namespace AppLogic
{
    class CreditCard : Card
    {
        #region konstruktory
        /// <summary>
        /// Tworzy nową kartę kredytową o podanych parametrach
        /// </summary>
        /// <param name="number">Numer karty</param>
        /// <param name="owner">Właściciel karty</param>
        /// <param name="isActive">Stan karty (czy jest aktywna)</param>
        /// <param name="balance">Ilość środków na karcie</param>
        /// <param name="creditLimit">Maksymalny debet (wartość bezwzględna)</param>
        internal CreditCard(string number, Client owner, bool isActive, decimal balance, decimal creditLimit =1000) : base(number, owner, isActive, balance)
        {
            this.creditLimit = -creditLimit;
        }

        /// <summary>
        /// Tworzy nową kartę kredytową o podanych parametrach
        /// </summary>
        /// <param name="number">Numer karty</param>
        /// <param name="owner">Właściciel karty</param>
        /// <param name="creditLimit">Maksymalny debet (wartość bezwzględna)</param>
        internal CreditCard(string number, Client owner, decimal creditLimit = 1000) : base(number, owner, true, 0)
        {
            this.creditLimit = -creditLimit;
        }
        #endregion

        #region pola i właściwości
        internal decimal CreditLimit { get { return -1 * (creditLimit); }}
        private decimal creditLimit;
        #endregion

        #region metody
        /// <summary>
        /// Dokonuje na karcie transakcji na podaną kwotę
        /// </summary>
        /// <param name="amount">Kwota transakcji</param>
        /// <exception cref="InsufficientCardBalanceException"/>
        internal override void MakeTransaction(decimal amount)
        {
            if(balance + amount < creditLimit)
                throw new InsufficientCardBalanceException("Kwota transakcji przekracza dopuszczalny limit karty kredytowej", Number, amount);
            balance += amount;
            balance *= 100;
            Math.Round(balance);
            balance /= 100;
        }
        /// <summary>
        /// autoryzuje transakcję na podaną kwotę
        /// </summary>
        /// <param name="amount">Kwota transakcji</param>
        /// <returns>Wynik autoryzacji</returns>
        internal override BankActionResult Authorize(decimal amount)
        {
            if (balance + amount < creditLimit)
                return BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE;
            return BankActionResult.SUCCESS;
        }
        #endregion
    }
}
