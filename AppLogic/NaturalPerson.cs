using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    internal class NaturalPerson : Client
    {
        public String secondName = "";

        public NaturalPerson(String name, String secondName, String PESEL) : base(name, PESEL)
        {
            this.secondName = secondName;
        }
    }
}
