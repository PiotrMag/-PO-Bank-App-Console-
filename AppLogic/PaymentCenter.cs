using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class PaymentCenter
    {
        public PaymentCenter()
        {
            list = new List<Client>();
        }
        private List<Client> list;

        public List<Client> GetClients()
        {
            return new List<Client>(list);
        }
    }
}
