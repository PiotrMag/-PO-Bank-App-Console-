using System;

namespace AppLogic
{
    #region wyjątki dot. kart
    class InsufficientCardBalanceException : Exception
    {
        internal string CardNumber { get; }
        internal decimal Amount { get; }
        internal InsufficientCardBalanceException(string message) : this(message, "null", 0) { }
        internal InsufficientCardBalanceException(string message, string cardNumber, decimal amount) : base(message)
        {
            CardNumber = cardNumber;
            Amount = amount;
        }
    }
    class NoSuchCardException : Exception
    {
        internal NoSuchCardException(string message, string cardNumber) : base(message)
        {
            CardNumber = cardNumber;
        }
        internal string CardNumber;
    }
    class NotEmptyAccountException : Exception
    {
        internal NotEmptyAccountException(string message, decimal amount, string cardNumber) : base(message)
        {
            Amount = amount;
            CardNumber = cardNumber;
        }
        internal string CardNumber { get; }
        internal decimal Amount { get; }
    }
    #endregion

    #region wyjątki dot. banków
    class BankContainsActiveCardsException : Exception
    {
        internal BankContainsActiveCardsException(string message) : base(message)
        {

        }
    }
    class BankAlreadyExistsException : Exception
    {
        internal BankAlreadyExistsException(string message) : base(message)
        {

        }
    }
    class NoSuchBankException : Exception
    {
        internal NoSuchBankException(string message, string name) : base(message)
        {
            Name = name;
        }
        internal string Name;
    }
    #endregion

    #region wyjątki dot. klientów
    class WrongUserException : Exception
    {
        internal WrongUserException(string message) : base(message)
        { }
    }
    class NullUserException : WrongUserException
    {
        internal NullUserException(string message) : base(message)
        { }
    }
    class UserAlreadyExistsException : WrongUserException
    {
        internal UserAlreadyExistsException(string message) : base(message)
        {

        }
    }
    #endregion

    #region wyjątki dot. transakcji
    class TransactionDeniedException : Exception
    {
        internal TransactionDeniedException(string message) : base(message)
        {

        }
    }
    class WrongSumException : Exception
    {
        internal WrongSumException(string message) : base(message)
        {

        }
    }
    #endregion

    #region wyjątki dot. bazy danych
    class NoSuchFileException : Exception
    {
        internal string FilePath { get; }
        internal NoSuchFileException(string message, string filePath) : base(message)
        {
            FilePath = filePath;
        }
    }
    class DBNotBoundException : Exception
    {
        internal string DBFilePath { get; }
        internal DBNotBoundException(string message, string dbFilePath) : base(message)
        {
            DBFilePath = dbFilePath;
        }
        internal DBNotBoundException(string message) : base(message) { }
    }
    #endregion
}
