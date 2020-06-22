using AppLogic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy DelBank.xaml
    /// </summary>
    public partial class DelBank : Page
    {
        public DelBank()
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
                PaymentCenter.Instance.DeleteBank(name.Text);
            }
            catch(BankContainsActiveCardsException ex)
            {
                success = false;
                MessageBox.Show(ex.Message);
            }
            catch(NoSuchBankException ex2)
            {
                success = false;
                MessageBox.Show(ex2.Message);
            }
            catch(NotEmptyAccountException ex3)
            {
                success = false;
                MessageBox.Show(ex3.Message + "\r\n" + ex3.CardNumber + "\r\n" + ex3.Amount);
            }
            if (success)
            {
                MessageBox.Show("Pomyślnie usunięto bank");
                var mainMenu = new MainMenu();
                NavigationService.Navigate(mainMenu);
            }
        }
    }
}
