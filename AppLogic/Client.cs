using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    abstract public class Client
    {
        public String name;
        public String number;
        public bool IsActive { get; set; }

        public Client(String name, String number)
        {
            this.name = name;
            this.number = number;
        }
    }
}
