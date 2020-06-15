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
    /// Logika interakcji dla klasy DelCard.xaml
    /// </summary>
    public partial class DelCard : Page
    {
        public DelCard()
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
                PaymentCenter.Instance.DeleteCardRequest(number.Text);
            }
            catch(NoSuchCardException ex)
            {
                success = false;
                MessageBox.Show(ex.Message);
            }
            catch(NotEmptyAccountException ex2)
            {
                success = false;
                MessageBox.Show(ex2.Message);
            }
            if (success)
            {
                MessageBox.Show("Pomyślnie usunięto kartę");
                var mainMenu = new MainMenu();
                NavigationService.Navigate(mainMenu);
            }
        }
    }
}
