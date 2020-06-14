using AppLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            string Path = null;
            InitializeComponent();
            PaymentCenter.Instance.InitDB(Path, "Banki");
            PaymentCenter.Instance.InitDB(Path, "Klienci");
            PaymentCenter.Instance.InitDB(Path, "Transakcje");
            PaymentCenter.Instance.LoadSystemState(Path);
        }
        private void Transaction(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction();
            NavigationService.Navigate(transaction);
        }

        private void ClientPanel(object sender, RoutedEventArgs e)
        {
            var clientPanel = new ClientPanel();
            NavigationService.Navigate(clientPanel);
        }

        private void Database(object sender, RoutedEventArgs e)
        {
            var database = new Database();
            NavigationService.Navigate(database);
        }

        private void SaveNQuit(object sender, RoutedEventArgs e)
        {
            string Path = null;
            if (!PaymentCenter.Instance.SaveSystemState(Path))
                MessageBox.Show("Wystąpił błąd zapisu");
            else
                Application.Current.Shutdown();
        }
    }
}
