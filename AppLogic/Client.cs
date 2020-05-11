using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    abstract public class Client
    {
        public string name;
        public string number;
        public bool IsActive { get; set; }

        public Client(string name, string number)
        {
            this.name = name;
            this.number = number;
        }
    }
}
