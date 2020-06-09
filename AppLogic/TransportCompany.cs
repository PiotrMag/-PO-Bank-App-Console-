using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    internal class TransportCompany : Company
    {
        public TransportCompany(string name, string NIP) : base(name, NIP, ClientType.TransportCompany) { }
    }
}
