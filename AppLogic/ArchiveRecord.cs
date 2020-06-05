using System;
using System.Dynamic;

namespace AppLogic
{
    public class ArchiveRecord
    {
        // Dane na temat firmy
        public string CompanyName { get; }
        public string CompanyType { get; }
        public string CompanyCardID { get; }
        public string CompanyCardType { get; }
        public string CompanyBankName { get; }
        public string CompanyBankID { get; }

        // Dane na temat klienta
        public string ClientName { get; }
        public string ClientID { get; }
        public string ClientCardID { get; }
        public string ClientCardType { get; }
        public string ClientBankName { get; }
        public string ClientBankID { get; }

        // Dane o trnsakcji
        public double Amount { get; }
        public BankActionResult Result { get; }

        /// <summary>
        /// Tworzy obiekt reprezentujący wpis, który może zostać obsłuzony przez Archive. Przechowuje on informacje o pojedynczym rekordzie (transakcji)
        /// </summary>
        /// <param name="companyName">Nazwa firmy</param>
        /// <param name="companyType">Typ firmy</param>
        /// <param name="companyCardID">ID firmy</param>
        /// <param name="companyCardType">Typ karty firmy</param>
        /// <param name="companyBankName">Nazwa banku, w którym firma ma kartę</param>
        /// <param name="companyBankID">ID banku, w którym firma ma kartę</param>
        /// <param name="clientName">Nazwa klienta</param>
        /// <param name="clientID">ID klienta</param>
        /// <param name="clientCardID">ID karty klienta</param>
        /// <param name="clientCardType">Typ karty klienta</param>
        /// <param name="clientBankName">Nazwa banku, w któryma klienta ma kratę</param>
        /// <param name="clientBankID">ID banku, w którym klient ma kartę</param>
        /// <param name="amount">Wielkość transakcji</param>
        /// <param name="bankActionResult">Wynik transakcji</param>
        public ArchiveRecord(string companyName, string companyType, string companyCardID, string companyCardType, string companyBankName, string companyBankID, 
                                string clientName, string clientID, string clientCardID, string clientCardType, string clientBankName, string clientBankID,
                                double amount, BankActionResult bankActionResult)
        {
            CompanyName = companyName == null ? companyName : "NULL";
            CompanyType = companyType == null ? companyType : "NULL";
            CompanyCardID = companyCardID == null ? companyCardID : "NULL";
            CompanyCardType = companyCardType == null ? companyCardType : "NULL";
            CompanyBankName = companyBankName == null ? companyBankName : "NULL";
            CompanyBankID = companyBankID == null ? companyBankID : "NULL";
            ClientName = clientName == null ? clientName : "NULL";
            ClientID = clientID == null ? clientID : "NULL";
            ClientCardID = clientCardID == null ? clientCardID : "NULL";
            ClientCardType = clientCardType == null ? clientCardType : "NULL";
            ClientBankName = clientBankName == null ? clientBankName : "NULL";
            ClientBankID = clientBankID == null ? clientBankID : "NULL";
            Amount = amount;
            Result = bankActionResult;
        }
    }
}

