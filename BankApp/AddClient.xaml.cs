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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppLogic;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddClient.xaml
    /// </summary>
    public partial class AddClient : Page
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            var clientPanel = new ClientPanel();
            NavigationService.Navigate(clientPanel);
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            try
            {
                bool success = int.TryParse(number.Text, out int Number);
                if (!success) MessageBox.Show("Błędny numer PESEL/NIP/REGON/KRS");
                switch(box.Text)
                {
                    case "Osoba fizyczna":
                        PaymentCenter.Instance.AddClient(number.Text, name.Text, ClientType.NaturalPerson);
                        break;
                    case "Firma transportowa":
                        PaymentCenter.Instance.AddClient(number.Text, name.Text, ClientType.TransportCompany);
                        break;
                    case "Sklep":
                        PaymentCenter.Instance.AddClient(number.Text, name.Text, ClientType.Shop);
                        break;
                    case "Zakład usługowy":
                        PaymentCenter.Instance.AddClient(number.Text, name.Text, ClientType.ServiceCompany);
                        break;
                    default:
                        MessageBox.Show("Nie podano typu klienta");
                        break;
                }
            }
            catch(UserAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
