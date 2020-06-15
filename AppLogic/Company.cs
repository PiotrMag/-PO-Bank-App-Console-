using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    abstract public class Company : Client
    {
        public Company(string name, string NIP, ClientType clientType) : base(name, NIP, clientType)
        {

        }

        public override bool Equals(object obj)
        {
            if (!(obj is Company))
                return false;
            if (this.Name == ((Company)obj).Name && this.Number == ((Company)obj).Number)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return int.Parse(Number);
        }
    }
}
