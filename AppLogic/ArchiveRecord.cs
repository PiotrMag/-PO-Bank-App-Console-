namespace AppLogic
{
    class ArchiveRecord
    {
        #region właściwości rekordu
        // Dane klienta
        public string FromName { get; }
        public string FromID { get; }
        public string FromType { get; }
        public string FromCardID { get; }
        public string FromCardType { get; }
        public string FromBankName { get; }
        public string FromBankID { get; }

        // Dane firmy
        public string ToName { get; }
        public string ToID { get; }
        public string ToType { get; }
        public string ToCardID { get; }
        public string ToCardType { get; }
        public string ToBankName { get; }
        public string ToBankID { get; }

        // Dane transakcji
        public decimal Amount { get; }
        public BankActionResult Result { get; }
        #endregion

        #region metody
        /// <summary>
        /// Tworzy obiekt reprezentujący wpis transakcji, który może zostać wykorzystany przez klasę Archive.
        /// </summary>
        /// <param name="fromName">Nazwa klienta</param>
        /// <param name="fromId">Id klienta</param>
        /// <param name="fromType">Typ klienta</param>
        /// <param name="fromCardID">ID klienta</param>
        /// <param name="fromCardType">Typ karty klienta</param>
        /// <param name="fromBankName">Nazwa banku, w którym klient ma kartę</param>
        /// <param name="fromBankID">ID banku, w którym klient ma kartę</param>
        /// <param name="toName">Nazwa firmy</param>
        /// <param name="toID">ID firmy</param>
        /// <param name="toType">Typ firmy</param>
        /// <param name="toCardID">ID karty firmy</param>
        /// <param name="toCardType">Typ karty firmy</param>
        /// <param name="toBankName">Nazwa banku, w któryma firma ma kratę</param>
        /// <param name="toBankID">ID banku, w którym firma ma kartę</param>
        /// <param name="amount">Kwota transakcji</param>
        /// <param name="bankActionResult">Wynik transakcji</param>
        internal ArchiveRecord(string fromName, string fromId, string fromType, string fromCardID, string fromCardType,
                                string fromBankName, string fromBankID, string toName, string toID, string toType,
                                string toCardID, string toCardType, string toBankName, string toBankID, decimal amount,
                                BankActionResult bankActionResult)
        {
            FromName = fromName ?? "NULL";
            FromID = fromId ?? "NULL";
            FromType = fromType ?? "NULL";
            FromCardID = fromCardID ?? "NULL";
            FromCardType = fromCardType ?? "NULL";
            FromBankName = fromBankName ?? "NULL";
            FromBankID = fromBankID ?? "NULL";
            ToName = toName ?? "NULL";
            ToID = toID ?? "NULL";
            ToType = toType ?? "NULL";
            ToCardID = toCardID ?? "NULL";
            ToCardType = toCardType ?? "NULL";
            ToBankName = toBankName ?? "NULL";
            ToBankID = toBankID ?? "NULL";
            Amount = amount;
            Result = bankActionResult;
        }
        #endregion
    }
}

