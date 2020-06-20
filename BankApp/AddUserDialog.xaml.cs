using AppLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static AppLogic.Card;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddUserDialog.xaml
    /// </summary>
    public partial class AddUserDialog : Window
    {
        public AddUserDialog(string clientID, CardType type, string bankName)
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
