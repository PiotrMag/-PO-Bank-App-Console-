using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy ClientPanel.xaml
    /// </summary>
    public partial class ClientPanel : Page
    {
        public ClientPanel()
        {
            InitializeComponent();
        }

        private void AddBank(object sender, RoutedEventArgs e)
        {
            var addBank = new AddBank();
            NavigationService.Navigate(addBank);
        }

        private void DelBank(object sender, RoutedEventArgs e)
        {
            var delBank = new DelBank();
            NavigationService.Navigate(delBank);
        }

        private void DelClient(object sender, RoutedEventArgs e)
        {
            var delClient = new DelClient();
            NavigationService.Navigate(delClient);
        }

        private void AddCard(object sender, RoutedEventArgs e)
        {
            var addCard = new AddCard();
            NavigationService.Navigate(addCard);
        }

        private void DelCard(object sender, RoutedEventArgs e)
        {
            var delCard = new DelCard();
            NavigationService.Navigate(delCard);
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            var mainMenu = new MainMenu();
            NavigationService.Navigate(mainMenu);
        }
    }
}
