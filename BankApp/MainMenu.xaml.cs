using AppLogic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {

        public MainMenu()
        {
            InitializeComponent();
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
            if (!PaymentCenter.Instance.SaveSystemState(MainWindow.SystemStatePath))
                MessageBox.Show("Wystąpił błąd zapisu");
            else
                Application.Current.Shutdown();
        }

        private void Companies(object sender, RoutedEventArgs e)
        {
            var companies = new Companies();
            NavigationService.Navigate(companies);
        }
    }
}
