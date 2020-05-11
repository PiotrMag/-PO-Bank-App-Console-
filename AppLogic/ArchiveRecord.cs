using System;
using System.Dynamic;

namespace AppLogic
{
    public class ArchiveRecord
    {
        public String CompanyName { get; }
        public String CompanyType { get; }
        public String BankName { get; }
        public int CardID { get; }
        public String OwnerName { get; }
        public double Amount { get; }
        public BankActionResult Result { get; }

        public ArchiveRecord()
        {

        }
    }
}

