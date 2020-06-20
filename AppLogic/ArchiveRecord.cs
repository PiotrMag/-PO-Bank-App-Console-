namespace AppLogic
{
    class ArchiveRecord
    {
        #region właściwości rekordu
        // Dane klienta
        internal string FromName { get; }
        internal string FromID { get; }
        internal string FromType { get; }
        internal string FromCardID { get; }
        internal string FromCardType { get; }
        internal string FromBankName { get; }
        internal string FromBankID { get; }

        // Dane firmy
        internal string ToName { get; }
        internal string ToID { get; }
        internal string ToType { get; }
        internal string ToCardID { get; }
        internal string ToCardType { get; }
        internal string ToBankName { get; }
        internal string ToBankID { get; }

        // Dane transakcji
        internal decimal Amount { get; }
        internal BankActionResult Result { get; }
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

