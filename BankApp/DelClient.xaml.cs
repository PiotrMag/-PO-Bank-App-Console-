using AppLogic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy DelClient.xaml
    /// </summary>
    public partial class DelClient : Page
    {
        public DelClient()
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
            bool success = true;
            try
            {
                PaymentCenter.Instance.DeleteClientRequest(number.Text);
            }
            catch(WrongUserException ex)
            {
                success = false;
                MessageBox.Show(ex.Message);
            }
            catch(NotEmptyAccountException ex2)
            {
                success = false;
                MessageBox.Show(ex2.Message + "\r\n" + ex2.Amount);
            }
            if (success)
            {
                MessageBox.Show("Pomyślnie usunięto klienta");
                var mainMenu = new MainMenu();
                NavigationService.Navigate(mainMenu);
            }
        }
    }
}
