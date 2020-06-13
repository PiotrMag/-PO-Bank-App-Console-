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

        }

        private void Database(object sender, RoutedEventArgs e)
        {

        }

        private void SaveNQuit(object sender, RoutedEventArgs e)
        {

        }
    }
}
