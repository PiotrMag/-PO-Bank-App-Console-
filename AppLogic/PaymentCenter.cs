using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static AppLogic.Card;

namespace AppLogic
{

    /// <summary>
    /// Klasa PaymentCenter zaimplementowana na bazie Singleton Pattern
    /// Nie jest to rozwiazanie wielowatkowe
    /// </summary>
    public sealed class PaymentCenter
    {
        private string dbFilePath;
        private bool isDBAvailable;

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
            bankList = new List<Bank>();
        }
        #endregion

        /// <summary>
        /// Metoda, którą należy wywołac przed wykonywaniem jakichkolwiek czynności związanych z bazą danych
        /// </summary>
        /// <param name="dbFilePath">Ścieżka do pliku z bazą danych (jeżeli jej nie ma, to zostanie utworzona)</param>
        /// <param name="dbTableName">Nazwa tabeli (jeżeli jej nie ma, to zostanie utworzona)</param>
        public void InitDB(string dbFilePath)
        {
            this.dbFilePath = dbFilePath;

            if (!Archive.CheckIfDBPresent(dbFilePath))
            {
                isDBAvailable = false;
                try
                {
                    Archive.CreateDBAndTable(dbFilePath);
                    isDBAvailable = true;
                }
                catch (SqliteException e)
                {
                    throw e;
                }
            }
            else
            {
                isDBAvailable = true;
            }
        }

        private readonly List<Bank> bankList;

        #region przeszukiwanie archiwum
        /// <summary>
        /// Zwraca liste rekordów archiwum spełniających podane zapytanie
        /// </summary
        /// <param name="query">Zapytanie do wykonania w archiwum</param>
        /// <returns>Zwraca te rekordy, które pasowały do zapytania SQLite</returns>
        public List<ArchiveRecord> SearchArchives(string query)
        {
            if (this.dbFilePath == null || !this.isDBAvailable)
                if (this.dbFilePath != null)
                    throw new DBNotBound("Baza danych istnieje, ale nie udało się skożystac z tabeli (tabela może nie istnieć)", dbFilePath);
                else
                    throw new DBNotBound("Nie można uzyskać dostępu do bazy danych");
            List<ArchiveRecord> data = null;
            try
            {
                data = Archive.ExecuteSQLQuery(dbFilePath, query);
            }
            catch (SqliteException e)
            {
                throw e;
            }
            return data;
        }
        #endregion

        #region logowanie płatności
        /// <summary>
        /// Dodaje wpis o transakcji do lokalnego archiwum
        /// </summary>
        /// <param name="fromCard">Karta, z której próbowano pobrać środki</param>
        /// <param name="fromBankName">Nazwa banku, w którym jest karta, z której próbowano pobrac środki</param>
        /// <param name="fromBankID">ID banku, w którym jest karta, z której próbowano pobrac środki</param>
        /// <param name="toCard">Karta, na którą próbowano wpłacić środki</param>
        /// <param name="toBankName">Nazwa banku, w którym jest karta, na którą przelać środki</param>
        /// <param name="toBankID">ID banku, w którym jest karta, na którą próbowano przelać środki</param>
        /// <param name="amount">Kwota</param>
        /// <param name="result">Wynik wykonania transakcji (czy wystąpił błąd, jeśli tak, to jaki)</param>
        public void LogInArchive(Card fromCard, Bank fromBank, Card toCard, Bank toBank, decimal amount, BankActionResult result)
        {
            if (isDBAvailable && dbFilePath != null)
            {
                Archive.AddRecord(dbFilePath,
                    new ArchiveRecord(fromCard.Owner.Name,
                                    fromCard.Owner.Number,
                                    fromCard.Owner.ClientType.ToString("g"),
                                    fromCard.Number,
                                    fromCard.Type.ToString("g"),
                                    fromBank.Name,
                                    fromBank.Id.ToString(),
                                    toCard.Owner.Name,
                                    toCard.Owner.Number,
                                    toCard.Owner.ClientType.ToString("g"),
                                    toCard.Number,
                                    toCard.Type.ToString("g"),
                                    toBank.Name,
                                    toBank.Id.ToString(),
                                    amount,
                                    result));
            }
            else
            {
                throw new DBNotBound("Nie połączonon z bazą danych");
            }
        }

        public void PrepareArchiveLog(decimal amount, BankActionResult result, string fromCardNumber = null, string toCardNumber = null)
        {
            LogInArchive(FindCardByNr(fromCardNumber), FindBankByCardNr(fromCardNumber), FindCardByNr(toCardNumber), FindBankByCardNr(toCardNumber), amount, result);
        }
        #endregion

        #region przegladanie firm
        /// <summary>
        /// Zwraca wszystkie firmy/sklepy. Unikalność firmy/sklepu jest określana na podstawie nazwy i numeru firmy/sklepu
        /// </summary>
        /// <returns>Lista typu Company</returns>
        public List<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();

            foreach (Bank b in bankList)
            {
                foreach (Card c in b.Cards)
                {
                    if (c.Owner is Company company)
                    {
                        if (!companies.Contains(c.Owner))
                        {
                            companies.Add(company);
                        }
                    }
                }
            }

            return companies;
        }
        #endregion
        /// <summary>
        /// Zwraca wszystkich klientów. Unikalność klienta jest określana na podstawie nazwy i numeru klienta
        /// </summary>
        /// <returns>Lista typu Client</returns>
        public List<Client> GetClients()
        {
            List<Client> list = new List<Client>();
            foreach (Bank b in bankList)
            {
                foreach (Card c in b.Cards)
                {
                    if (!list.Contains(c.Owner))
                    {
                        list.Add(c.Owner);
                    }
                }
            }
            return list;
        }

        #region zapis/odczyt stanu centrum
        /// <summary>
        /// Zapisuje stan systemu w pliku w formacie XML
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku do zapisania</param>
        /// <returns>Zwraca true, jeżeli udało się zapisać bez problemu, a false jeżeli sie nie udało</returns>
        public bool SaveSystemState(String filePath)
        {
            StringBuilder xmlContent = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(xmlContent))
            {
                writer.WriteStartDocument();
                // zapisywanie stanu banków
                foreach (Bank bank in bankList)
                {
                    string bankName = bank.Name;
                    int bankId = bank.Id;
                    if (bankName == null || bankId < 0)
                        continue; // TODO: może trzeba zmienić, żeby wyrzucało błąd????
                    writer.WriteStartElement("bank");
                    writer.WriteAttributeString("name", bankName);
                    writer.WriteAttributeString("id", bankId.ToString());

                    foreach (Card card in bank.Cards)
                    {
                        string cardNumber = card.Number;
                        Client cardOwner = card.Owner;
                        if (cardNumber == null || cardOwner == null)
                            continue; // TODO: może lepiej przerobić, żeby wyrzucało wyjątek????
                        CardType cardType;
                        if (card is CreditCard)
                            cardType = CardType.CreditCard;
                        else if (card is DebitCard)
                            cardType = CardType.DebitCard;
                        else if (card is ATMCard)
                            cardType = CardType.ATMCard;
                        else
                            continue; //TODO: może lepiej wyrzucić wyjątek????

                        writer.WriteStartElement("card");
                        writer.WriteAttributeString("number", cardNumber);
                        writer.WriteAttributeString("ownerName", cardOwner.Name);
                        writer.WriteAttributeString("ownerNumber", cardOwner.Number.ToString());
                        writer.WriteAttributeString("ownerType", cardOwner.ClientType.ToString("g"));
                        writer.WriteAttributeString("cardType", cardType.ToString("g"));
                        writer.WriteAttributeString("isActive", card.IsActive.ToString());
                        writer.WriteAttributeString("balance", card.Balance.ToString());

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }
                // zapisywanie stanu archiwum
                // .........
                // .........
            }

            return FileHandling.WriteFile(filePath, xmlContent.ToString(), false);
        }

        /// <summary>
        /// Sprawdza, czy bank o danym id jest na liście bankList w PaymentCenter
        /// </summary>
        /// <param name="bankId">ID banku</param>
        /// <returns>czy dany bank jest na liście bankList</returns>
        private bool IsBankOnTheList(int bankId)
        {
            if (bankId < 0)
                return false;

            foreach (Bank b in bankList)
            {
                if (b.Id == bankId)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Odczytuje stan systemu z pliku o podanej ścieżce.
        /// UWAGA: Obecny stan systemu zostanie utracony na rzecz nowego stanu
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku do odczytania</param>
        /// <returns>Zwraca true, jeżeli udało się poprawnie załadować stan systemu, a false, jeżeli wystąpił błąd</returns>
        public bool LoadSystemState(string filePath)
        {
            Stream fileStream = FileHandling.GetReadingStream(filePath);
            if (fileStream == null)
                throw new NoSuchFileException("Nie znaleziono podanego pliku do czytania", filePath);

            // Czyszczenie listy banków
            bankList.Clear();

            // XMLParser (reader)
            using (XmlReader reader = XmlReader.Create(fileStream))
            {
                string currentBankName = null, currentBankId = null;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "bank")
                    {
                        currentBankName = reader.GetAttribute("name");
                        currentBankId = reader.GetAttribute("id");
                    }

                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "card")
                    {
                        if (reader.HasAttributes)
                        {
                            string clientName, clientNumber, cardNumber, cardLimitString;
                            decimal cardLimit = 0, balance = 0;
                            bool isActive = false;
                            ClientType clientType;
                            CardType cardType;
                            //TODO: dodać wczytywanie isActive i balance

                            clientName = reader.GetAttribute("ownerName");
                            clientNumber = reader.GetAttribute("ownerNumber");
                            clientType = (ClientType)int.Parse(reader.GetAttribute("ownerType"));
                            cardNumber = reader.GetAttribute("number");
                            cardType = (CardType)int.Parse(reader.GetAttribute("cardType"));
                            cardLimitString = reader.GetAttribute("cardLimit");
                            isActive = bool.Parse(reader.GetAttribute("isActive"));
                            balance = decimal.Parse(reader.GetAttribute("balance"));

                            if (cardLimitString != null)
                                cardLimit = decimal.Parse(cardLimitString);

                            Client owner;

                            if (clientType == ClientType.NaturalPerson)
                                owner = new NaturalPerson(clientName, clientNumber);
                            else if (clientType == ClientType.ServiceCompany)
                                owner = new ServiceCompany(clientName, clientNumber);
                            else if (clientType == ClientType.Shop)
                                owner = new Shop(clientName, clientNumber);
                            else if (clientType == ClientType.ServiceCompany)
                                owner = new ServiceCompany(clientName, clientNumber);
                            else
                                continue;

                            Card newCard;

                            if (cardType == CardType.CreditCard)
                                newCard = new CreditCard(cardNumber, owner, isActive, balance, cardLimit);
                            else if (cardType == CardType.DebitCard)
                                newCard = new DebitCard(cardNumber, owner, isActive, balance);
                            else if (cardType == CardType.ATMCard)
                                newCard = new ATMCard(cardNumber, owner, isActive, balance);
                            else
                                continue;

                            if (!IsBankOnTheList(currentBankId == null ? -1 : int.Parse(currentBankId)))
                            {
                                Bank newBank;

                                if (currentBankId == null || int.Parse(currentBankId) < 0)
                                {
                                    newBank = new Bank(currentBankName ?? "UNKNOWN");
                                    currentBankId = newBank.Id.ToString();
                                    currentBankName = newBank.Name;
                                }
                                else
                                {
                                    newBank = new Bank(currentBankName, int.Parse(currentBankId), true);
                                }

                                bankList.Add(newBank);
                            }

                            AddNewCardRequest(newCard, int.Parse(currentBankId));
                        }
                    }

                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "bank")
                    {
                        currentBankName = null;
                        currentBankId = null;
                    }
                }
            }
            // ....

            return true;
        }
        #endregion

        #region dokonanie transakcji
        /// <summary>
        /// Metoda wysyłająca prośbę o dokonanie transakcji do banków obsługujących dane karty
        /// </summary>
        /// <param name="fromCardNumber">Karta, z której ma zostać zabrana kwota</param>
        /// <param name="toCardNumber">Karta, na która ma zostaś wpłacona kwota</param>
        /// <param name="amount">Kwota</param>
        /// <returns>Wynik wykonania transakcji</returns>
        public void MakeTransactionRequest(string fromCardNumber, string toCardNumber, decimal amount)
        {
            if (fromCardNumber.Length < 5 || toCardNumber.Length < 5) throw new TransactionDeniedException("Nieprawidłowy numer karty");
            int id1 = 9999 - int.Parse(fromCardNumber.Remove(4));
            int id2 = 9999 - int.Parse(toCardNumber.Remove(4));
            int count = 0;
            foreach (var bank in bankList)
            {
                if (bank.Id == id1 && bank.Authorize(fromCardNumber, amount) == BankActionResult.SUCCESS)
                    count++;
            }
            if (count == 0) throw new TransactionDeniedException("Karta wyjściowa nie przeszła autoryzacji");
            foreach (var bank in bankList)
            {
                if (bank.Id == id2 && bank.Authorize(toCardNumber, amount) == BankActionResult.SUCCESS)
                    count++;
            }
            if (count == 1) throw new TransactionDeniedException("Karta wejściowa nie przeszła autoryzacji");
            if (count == 2)
            {
                foreach (var bank in bankList)
                {
                    if (bank.Id == id1)
                    {
                        bank.MakeTransaction(fromCardNumber, -amount);
                    }
                    if (bank.Id == id2)
                    {
                        bank.MakeTransaction(toCardNumber, amount);
                    }
                }
            }
        }

        /// <summary>
        /// prośba o dokonanie wpłaty/wypłady z konta
        /// </summary>
        /// <param name="number">numer karty</param>
        /// <param name="amount">kwota operacji</param>
        public void OneCardTransactionRequest(string number, decimal amount)
        {
            if (number.Length < 5) throw new NoSuchCardException("Nieprawidłowy numer karty", number);
            int id = 9999 - int.Parse(number.Remove(4));
            foreach (var bank in bankList)
            {
                if (bank.Id == id && bank.Authorize(number, amount) == BankActionResult.SUCCESS)
                {
                    bank.MakeTransaction(number, amount);
                }
                else if (bank.Id == id)
                    throw new InsufficientCardBalanceException("Brak środków na karcie");

            }
            throw new NoSuchCardException("Nie znaleziono karty o podanym numerze", number);
        }
        #endregion

        #region obsługa kart (dodawanie/usuwanie/sprawdzanie typu)
        /// <summary>
        /// Wysyła do banku prośbę o dodanie nowej karty bierzącemu klientowi
        /// </summary>
        /// <param name="client">Klient żądający dodania karty</param>
        /// <param name="type">Typ tworzonej karty</param>
        /// <returns>
        /// Obiekt utworzonej karty
        /// </returns>
        public Card AddNewCardRequest(string clientNr, CardType type, string bankName)
        {
            Card card = null;
            try
            {
                int bankId = FindBankByName(bankName);
                foreach (var bank in bankList)
                {
                    if (bankId == bank.Id)
                    {
                        card = bank.AddCard(FindClientByNr(clientNr), type);
                        break;
                    }
                }
            }
            catch (NullUserException ex)
            {
                throw ex;
            }
            catch (WrongUserException ex2)
            {
                throw ex2;
            }
            catch (NoSuchBankException ex3)
            {
                throw ex3;
            }
            return card;
        }

        private int FindBankByName(string bankName)
        {
            foreach (var bank in bankList)
            {
                if (bank.Name == bankName)
                    return bank.Id;
            }
            throw new NoSuchBankException("Nie znaleziono banku o podanej nazwie", bankName);
        }

        public void AddNewCardRequest(Card card, int bankId)
        {
            Bank bank = null;
            foreach (Bank b in bankList)
            {
                if (b.Id == bankId)
                {
                    bank = b;
                    break;
                }
            }

            if (bank == null)
                throw new NoSuchCardException("Nieprawidłowy numer karty", card.Number);//Exception("Probowano dodac karte do nieistniejacegeo banku"); //TODO: przerobić na odpowiedni typ Exception

            bank.AddCard(card);
        }

        private Card FindCardByNr(string nr)
        {
            if (nr == null) return null;
            Card card = null;
            int id = 9999 - int.Parse(nr.Remove(4, nr.Length - 4));
            foreach (Bank bank in bankList)
                if (bank.Id == id)
                    foreach (Card c in bank.Cards)
                        if (c.Number == nr)
                        {
                            card = c;
                            break;
                        }
            if (card != null)
                return card;
            throw new NoSuchCardException("Nie znaleziono karty", nr);
        }

        /// <summary>
        /// Wysyła do banku prośbę o usunięcie z systemu karty
        /// </summary>
        /// <param name="number">Numer usuwanej karty</param>
        public void DeleteCardRequest(string number)
        {
            int id = 9999 - int.Parse(number.Remove(4));
            try
            {
                bool removed = false;
                foreach (var bank in bankList)
                {
                    if (bank.Id == id)
                    {
                        bank.DeleteCard(number);
                        removed = true;
                        break;
                    }
                }
                if (!removed) throw new NoSuchCardException("Nie znaleziono karty o podanym numerze", number);
            }
            catch (NoSuchCardException ex)
            {
                throw ex;
            }
            catch (NotEmptyAccountException exception)
            {
                throw exception;
            }
        }

        #endregion

        private Client FindClientByNr(string nr)
        {
            Client client = null;
            List<Client> list = GetClients();
            foreach (Client c in list)
                if (c.Number == nr)
                {
                    client = c;
                    break;
                }
            if (client != null)
                return client;
            throw new WrongUserException("Nie znaleziono użytkownika");
        }

        /// <summary>
        /// Wysyła do wszystkich banków prośbę o usunięcie z systemu klienta, usuwa klienta z listy lokalnej
        /// </summary>
        /// <param name="client">Obiekt usuwanego klienta</param>
        public void DeleteClientRequest(string number)
        {
            try
            {
                List<Client> clientList = GetClients();
                Client client = FindClientByNr(number);
                foreach (var bank in bankList)
                {
                    bank.DeleteClient(client);
                }
                foreach (var c in clientList)
                {
                    if (c == client)
                    {
                        c.IsActive = false;
                        break;
                    }
                }
            }
            catch (NotEmptyAccountException ex)
            {
                throw ex;
            }
            catch (WrongUserException ex2)
            {
                throw ex2;
            }
        }


        private Bank FindBankByCardNr(string number)
        {
            if (number == null) return null;
            int nr = 9999 - int.Parse(number.Remove(4, number.Length - 4));
            foreach (var b in bankList)
            {
                if (b.Id == nr) return b;
            }
            return null;
        }
        public void AddBank(string name)
        {
            foreach (var bank in bankList)
            {
                if (bank.Name == name)
                    throw new BankAlreadyExistsException("Bank o podanej nazwie został dodany wcześniej");
            }
            bankList.Add(new Bank(name));
        }

        public void DeleteBank(string name)
        {
            foreach (var bank in bankList)
            {
                if (bank.Name == name)
                {
                    foreach (var card in bank.Cards)
                        if (card.IsActive)
                            throw new BankContainsActiveCardsException("Nie można usunąć - bank zawiera aktywne karty");
                    bank.IsActive = false;
                    break;
                }
            }
        }
    }
}
