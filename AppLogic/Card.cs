namespace AppLogic
{
    abstract class Card
    {
        #region enum typ karty
        /// <summary>
        /// Typ karty
        /// </summary>
        internal enum CardType
        {
            NULL = -1,
            CreditCard = 1,
            DebitCard = 2,
            ATMCard = 3,
        }
        #endregion

        #region konstruktory
        /// <summary>
        /// tworzy nową kartę płatniczą o podanych parametrach
        /// </summary>
        /// <param name="number">Numer karty</param>
        /// <param name="owner">Właściciel</param>
        /// <param name="isActive">Stan karty (czy jest aktywna)</param>
        /// <param name="balance">Ilość środków na karcie</param>
        internal Card(string number, Client owner, bool isActive, decimal balance)
        {
            Number = number;
            Owner = owner;
            IsActive = isActive;
            this.balance = balance;
        }
        #endregion

        #region właściwości
        internal Client Owner { get; }
        internal string Number { get; set; }
        internal bool IsActive { get; set; }
        internal decimal Balance { get { return balance; } }
        internal CardType Type { get; }
        protected decimal balance;
        #endregion

        #region metody abstrakcyjne
        /// <summary>
        /// Abstrakcyjna metoda, której zadaniem jest dokonanie transakcji na kartę
        /// </summary>
        /// <param name="amount">Kwota transakcji</param>
        /// <exception cref="InsufficientCardBalanceException">Wyrzuca wyjątek InsufficientCardBalanceException, jeżeli transakcja przekracza możliwości karty</exception>
        abstract internal void MakeTransaction(decimal amount);

        /// <summary>
        /// Autoryzuje transakcję na danej karcie na podaną kwotę
        /// </summary>
        /// <param name="amount">Kwota transakcji</param>
        /// <returns>Wynik autoryzacji</returns>
        abstract internal BankActionResult Authorize(decimal amount);
        #endregion

        #region przesłonięte metody klasy Object
        public override bool Equals(object obj)
        {
            if (!(obj is Card))
                return false;
            Card card = (Card)obj;
            if (card.Number == this.Number && card.Owner.Equals(this.Owner))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return int.Parse(Number);
        }
        #endregion
    }
}
