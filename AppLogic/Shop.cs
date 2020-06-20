namespace AppLogic
{
    class Shop : Company
    {
        #region konstruktory
        /// <summary>
        /// Tworzy obiekt Sklepu
        /// </summary>
        /// <param name="name">Nazwa firmy</param>
        /// <param name="NIP">Id firmy</param>
        internal Shop(string name, string NIP) : base(name, NIP, ClientType.Shop) { }
        #endregion
    }
}
