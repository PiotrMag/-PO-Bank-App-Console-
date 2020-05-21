using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public enum ClientType
    {
        Null = -1,
        NaturalPerson = 1,
        ServiceCompany = 2,
        Shop = 3,
        TransportCompany = 4,
    }
    abstract public class Client
    {
        public string Name { get; }
        public string Number { get; }
        public bool IsActive { get; set; }
        public ClientType clientType { get; }

        public Client(string name, string number, ClientType clientType)
        {
            this.Name = name;
            this.Number = number;
            this.clientType = clientType; 
        }
    }
}
