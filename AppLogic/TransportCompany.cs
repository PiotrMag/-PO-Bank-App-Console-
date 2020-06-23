namespace AppLogic
{
    class TransportCompany : Company
    {
        #region konstruktory
        /// <summary>
        /// Tworzy obiekt Firmy transportowej
        /// </summary>
        /// <param name="name">Nazwa firmy</param>
        /// <param name="NIP">Id firmy</param>
        internal TransportCompany(string name, string NIP) : base(name, NIP, ClientType.TransportCompany) { }
        #endregion
    }
}
