namespace AppLogic
{
    class ServiceCompany : Company
    {
        #region konstruktory
        /// <summary>
        /// Tworzy obiekt Przedsiębiorstwa usługowego
        /// </summary>
        /// <param name="name">Nazwa firmy</param>
        /// <param name="NIP">Id firmy</param>
        internal ServiceCompany(string name, string NIP) : base(name, NIP, ClientType.ServiceCompany) { }
        #endregion
    }
}
