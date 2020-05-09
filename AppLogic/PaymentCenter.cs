using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    /// <summary>
    /// Klasa PaymentCenter zaimplementowana na bazie Singleton Pattern
    /// Nie jest to rozwiazanie wielowatkowe
    /// </summary>
    public sealed class PaymentCenter
    {
        // Czesc kodu potrzebna do zaimplementowania Singleton Pattern
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
            companyList= new List<Client>();
            banksList = new List<Bank>();
        }
        //--------------------------------------------koniec Singleton

        private List<Client> companyList;
        private List<Bank> banksList;

        public List<Client> GetClients()
        {
            return new List<Client>(list);
        }

        /// <summary>
        /// Zwraca liste obiektow typu ArchiveRecord
        /// Kazdy element jest pojedynczym wpisem do lokalnego archiwum
        /// </summary
        /// <param name="query">Zapytanie do wykonania w archiwum</param>
        public List<ArchiveRecord> SearchArchives(String query) 
        {
            return null;
        }
    }
}
