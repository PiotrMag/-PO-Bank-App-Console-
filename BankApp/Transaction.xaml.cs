using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy Page1.xaml
    /// </summary>
    public partial class Transaction : Page
    {
        public Transaction()
        {
            InitializeComponent();
        }

        private void Withdraw(object sender, RoutedEventArgs e)
        {
            var withdraw = new Withdraw();
            NavigationService.Navigate(withdraw);
        }

        private void Payment(object sender, RoutedEventArgs e)
        {
            var payment = new Payment();
            NavigationService.Navigate(payment);
        }

        private void Transfer(object sender, RoutedEventArgs e)
        {
            var transfer = new Transfer();
            NavigationService.Navigate(transfer);
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            var mainMenu = new MainMenu();
            NavigationService.Navigate(mainMenu);
        }
    }
}
