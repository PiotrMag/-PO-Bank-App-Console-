using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class InsufficientCardBalanceException : Exception
    {
        public string CardNumber { get; }
        public double Amount { get; }
        public InsufficientCardBalanceException(string message) : this(message, "null", 0) { }
        public InsufficientCardBalanceException(string message, string cardNumber, double amount) : base(message)
        {
            this.CardNumber = cardNumber;
            this.Amount = amount;
        }
    }

    public class WrongUserException : Exception
    {
        public WrongUserException(string message) : base(message)
        { }
    }
    public class NullUserException : WrongUserException
    {
        public NullUserException(string message) : base(message)
        { }
    }

    public class NoSuchCardException : Exception
    {
        public NoSuchCardException(string message, string cardNumber) : base(message)
        {
            CardNumber = cardNumber;
        }
        public string CardNumber;
    }

    public class NotEmptyAccountException : Exception
    {
        public NotEmptyAccountException(string message, double amount, string cardNumber) : base(message)
        {
            Amount = amount;
            CardNumber = cardNumber;
        }
        public string CardNumber { get; }
        public double Amount { get; }
    }

    public class TransactionDeniedException : Exception
    {
        public TransactionDeniedException(string message) : base(message)
        {

        }
    }

    public class NoSuchFileException : Exception
    {
        public string FilePath { get; }
        public NoSuchFileException(string message, string filePath) : base(message)
        {
            this.FilePath = filePath;
        }
    }

    public class DBNotBound : Exception
    {
        public string DBFilePath { get; }
        public DBNotBound(string message, string dbFilePath) : base(message)
        {
            this.DBFilePath = dbFilePath;
        }
        public DBNotBound(string message) : base(message) { }
    }

    public class WrongSumException : Exception
    {
        public WrongSumException(string message) : base(message)
        {

        }
    }
}
