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

        private void AddClient(object sender, RoutedEventArgs e)
        {
            var addClient = new AddClient();
            NavigationService.Navigate(addClient);
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
