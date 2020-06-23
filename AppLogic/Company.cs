namespace AppLogic
{
    abstract class Company : Client
    {
        #region konstruktory
        /// <summary>
        /// Tworzy obiekt firmy o podanych parametrach
        /// </summary>
        /// <param name="name">Nazwa firmy</param>
        /// <param name="NIP">Id firmy</param>
        /// <param name="clientType">Typ firmy</param>
        internal Company(string name, string NIP, ClientType clientType) : base(name, NIP, clientType)
        {

        }
        #endregion

        #region przesłonięte metody klasy Object
        public override bool Equals(object obj)
        {
            if (!(obj is Company))
                return false;
            if (this.Name == ((Company)obj).Name && this.Number == ((Company)obj).Number)
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
