using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AppLogic
{
    public enum CardType
    {
        CreditCard,
        DebitCard,
        ATMCard
    }

    /// <summary>
    /// Klasa PaymentCenter zaimplementowana na bazie Singleton Pattern
    /// Nie jest to rozwiazanie wielowatkowe
    /// </summary>
    public sealed class PaymentCenter
    {
        #region Kod Singleton Pattern
        private static PaymentCenter instance = null;
        public static PaymentCenter Instance 
        {
            get 
            {
                if (instance == null) 
                    instance = new PaymentCenter();
                return instance;
            }
        }

        private PaymentCenter()
        {
            banksList = new List<Bank>();
        }
        #endregion

        private List<Bank> banksList;


        /// <summary>
        /// Zwraca liste obiektow typu ArchiveRecord
        /// Kazdy element jest pojedynczym wpisem do lokalnego archiwum
        /// </summary
        /// <param name="query">Zapytanie do wykonania w archiwum</param>
        public List<ArchiveRecord> SearchArchives(String query) 
        {
            return null;
        }

        #region logowanie płatności
        /// <summary>
        /// Dodaje wpis o transakcji do lokalnego archiwum
        /// </summary>
        /// <param name="fromCard">Karta, z której próbowano pobraś środki</param>
        /// <param name="toCard">Karta, na którą próbowano wpłacić środki</param>
        /// <param name="amount">Kwota</param>
        /// <param name="result">Wynik wykonania transakcji (czy wystąpił błąd, jeśli tak, to jaki)</param>
        public void LogInArchive(Card fromCard, Card toCard, double amount, String result)
        {

        }
        #endregion

        #region przegladanie firm
        /// <summary>
        /// Zwraca wszystkie firmy/sklepy
        /// </summary>
        /// <returns>Lista typu Company</returns>
        public List<Company> GetCompanies()
        {
            return new List<Company>();
        }
        #endregion

        #region zapis/odczyt stanu centrum
        /// <summary>
        /// Zapisuje stan systemu w pliku w formacie XML
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku do zapisania</param>
        public void SaveSystemState(String filePath)
        {
            return;
        }

        /// <summary>
        /// Odczytuje stan systemu z pliku o podanej ścieżce.
        /// UWAGA: Obecny stan systemu zostanie utracony na rzecz nowego stanu
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku do odczytania</param>
        /// <returns>Zwraca true, jeżeli udało się poprawnie załadować stan systemu, a false, jeżeli wystąpił błąd</returns>
        public bool LoadSystemState(String filePath)
        {
            string fileContent = FileHandling.ReadFile(filePath);
            if (fileContent == null)
                return false;

            // XML Parser (Reader)
            //
            //
            // ....

            return true;
        }
        #endregion

        #region dokonanie transakcji
        /// <summary>
        /// Metoda wysyłająca prośbę o dokonanie transakcji do banków obsługujących dane karty
        /// </summary>
        /// <param name="fromCard">Karta, z której ma zostać zabrana kwota</param>
        /// <param name="toCard">Karta, na która ma zostaś wpłacona kwota</param>
        /// <param name="amount">Kwota</param>
        /// <returns>Wynik wykonania transakcji</returns>
        public BankActionResult MakeTransactionRequest(Card fromCard, Card toCard, double amount)
        {
            return BankActionResult.NULL;
        }
        #endregion

        #region obsługa kart (dodawanie/usuwanie)
        /// <summary>
        /// Wysyła do banku prośbę o dodanie nowej karty bierzącemu klientowi
        /// </summary>
        /// <param name="client">Klient żądający dodania karty</param>
        /// <param name="type">Typ tworzonej karty</param>
        /// <returns>
        /// BankActionResult, ktory mowi o tym, czy akcja sie powiodla, czy nie
        /// </returns>
        public BankActionResult AddNewCardRequest(Client client, CardType type)
        {
            return BankActionResult.NULL;
        }

        /// <summary>
        /// Wysyła do banku prośbę o usunięcie z systemu karty
        /// </summary>
        /// <param name="number">Numer usuwanej karty</param>
        /// <returns>
        /// BankActionResult, ktory mowi o tym, czy akcja sie powiodla, czy nie
        /// </returns>
        public BankActionResult DeleteCardRequest(string number)
        {
            return BankActionResult.NULL;
        }
        #endregion

        #region obsługa klientów (dodawanie/usuwanie)
        /// <summary>
        /// Wysyła do banku prośbę o dodanie do systemu nowego klienta
        /// </summary>
        /// <param name="client">Obiekt dodawanego klienta</param>
        /// <returns></returns>
        public BankActionResult AddNewClientRequest(Client client)
        {
            return BankActionResult.NULL;
        }

        /// <summary>
        /// Wysyła do banku prośbę o usunięcie z systemu klienta
        /// </summary>
        /// <param name="client">Obiekt usuwanego klienta</param>
        /// <returns></returns>
        public BankActionResult DeleteClientRequest(Client client)
        {
            return BankActionResult.NULL;
        }
        #endregion
    }
}
