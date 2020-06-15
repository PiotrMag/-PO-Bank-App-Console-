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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if (success)
            {
                MessageBox.Show("Pomyślnie usunięto bank");
                var mainMenu = new MainMenu();
                NavigationService.Navigate(mainMenu);
            }
        }
    }
}
