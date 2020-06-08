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
        public ClientType ClientType { get; }

        public Client(string name, string number, ClientType clientType)
        {
            Name = name;
            Number = number;
            this.clientType = clientType; 
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Client))
                return false;
            Client client = (Client)obj;
            if (client.Name == this.Name && client.clientType == this.clientType && client.Number == this.Number)
                return true;
            return false;
        }
    }
}
