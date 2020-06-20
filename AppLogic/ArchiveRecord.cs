using System;
using System.Dynamic;

namespace AppLogic
{
    public class ArchiveRecord
    {
        // Dane na temat firmy
        public string FromName { get; }
        public string FromID { get; }
        public string FromType { get; }
        public string FromCardID { get; }
        public string FromCardType { get; }
        public string FromBankName { get; }
        public string FromBankID { get; }

        // Dane na temat klienta
        public string ToName { get; }
        public string ToID { get; }
        public string ToType { get; }
        public string ToCardID { get; }
        public string ToCardType { get; }
        public string ToBankName { get; }
        public string ToBankID { get; }

        // Dane o trnsakcji
        public decimal Amount { get; }
        public BankActionResult Result { get; }

        /// <summary>
        /// Tworzy obiekt reprezentujący wpis, który może zostać obsłuzony przez Archive. Przechowuje on informacje o pojedynczym rekordzie (transakcji)
        /// </summary>
        /// <param name="fromName">Nazwa firmy</param>
        /// <param name="fromId">Id firmy</param>
        /// <param name="fromType">Typ firmy</param>
        /// <param name="fromCardID">ID firmy</param>
        /// <param name="fromCardType">Typ karty firmy</param>
        /// <param name="fromBankName">Nazwa banku, w którym firma ma kartę</param>
        /// <param name="fromBankID">ID banku, w którym firma ma kartę</param>
        /// <param name="toName">Nazwa klienta</param>
        /// <param name="toID">ID klienta</param>
        /// <param name="toType">Typ klienta</param>
        /// <param name="toCardID">ID karty klienta</param>
        /// <param name="toCardType">Typ karty klienta</param>
        /// <param name="toBankName">Nazwa banku, w któryma klienta ma kratę</param>
        /// <param name="toBankID">ID banku, w którym klient ma kartę</param>
        /// <param name="amount">Wielkość transakcji</param>
        /// <param name="bankActionResult">Wynik transakcji</param>
        public ArchiveRecord(string fromName, string fromId, string fromType, string fromCardID, string fromCardType, string fromBankName, string fromBankID, 
                                string toName, string toID, string toType, string toCardID, string toCardType, string toBankName, string toBankID,
                                decimal amount, BankActionResult bankActionResult)
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
    }
}

