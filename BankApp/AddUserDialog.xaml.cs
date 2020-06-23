using AppLogic;
using System.Windows;
using static AppLogic.Card;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddUserDialog.xaml
    /// </summary>
    public partial class AddUserDialog : Window
    {
        internal AddUserDialog(string clientID, CardType type, string bankName)
        {
            ClientID = clientID;
            Type = type;
            BankName = bankName;
            InitializeComponent();
        }

        private readonly string ClientID;
        private readonly CardType Type;
        private readonly string BankName;

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Card card = null;
            switch (box.Text)
            {
                case "Sklep":
                    card = PaymentCenter.Instance.AddCardWithOwnerRequest(ClientID, Type, BankName, name.Text, ClientType.Shop);
                    break;
                case "Osoba fizyczna":
                    card = PaymentCenter.Instance.AddCardWithOwnerRequest(ClientID, Type, BankName, name.Text, ClientType.NaturalPerson);
                    break;
                case "Przedsiębiorstwo usługowe":
                    card = PaymentCenter.Instance.AddCardWithOwnerRequest(ClientID, Type, BankName, name.Text, ClientType.ServiceCompany);
                    break;
                case "Firma transportowa":
                    card = PaymentCenter.Instance.AddCardWithOwnerRequest(ClientID, Type, BankName, name.Text, ClientType.TransportCompany);
                    break;
            }
            MessageBox.Show("Dodano kartę o numerze: " + card.Number);
            Close();
        }
    }
}
