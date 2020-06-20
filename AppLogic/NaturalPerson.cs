namespace AppLogic
{
    class NaturalPerson : Client
    {
        #region konstruktory
        /// <summary>
        /// Tworzy obiekt osoby fizycznej o podanych parametrach
        /// </summary>
        /// <param name="name">Imię i Nazwisko</param>
        /// <param name="PESEL">Numer PESEL</param>
        internal NaturalPerson(string name, string PESEL) : base(name, PESEL, ClientType.NaturalPerson)
        {
            
        }
        #endregion
    }
}
