namespace AppLogic
{
    #region enum typ klienta
    /// <summary>
    /// Typ klienta
    /// </summary>
    enum ClientType
    {
        Null = -1,
        NaturalPerson = 1,
        ServiceCompany = 2,
        Shop = 3,
        TransportCompany = 4,
    }
    #endregion

    abstract class Client
    {
        #region konstruktory
        /// <summary>
        /// Tworzy obiekt klienta
        /// </summary>
        /// <param name="name">Nazwa klienta</param>
        /// <param name="number">Id klienta</param>
        /// <param name="clientType">Typ klienta</param>
        internal Client(string name, string number, ClientType clientType)
        {
            Name = name;
            Number = number;
            ClientType = clientType;
        }
        #endregion

        #region właściwości
        internal string Name { get; }
        internal string Number { get; }
        internal bool IsActive { get; set; }
        internal ClientType ClientType { get; }
        #endregion

        #region przesłonięte metody klasy Object
        public override bool Equals(object obj)
        {
            if (!(obj is Client))
                return false;
            Client client = (Client)obj;
            if (client.Name == Name && client.ClientType == ClientType && client.Number == Number)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return int.Parse(Number);
        }
        #endregion
    }
}
