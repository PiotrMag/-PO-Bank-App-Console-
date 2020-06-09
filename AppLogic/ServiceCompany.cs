using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    internal class ServiceCompany : Company
    {
        public ServiceCompany(string name, string NIP) : base(name, NIP, ClientType.ServiceCompany) { }
    }
}
