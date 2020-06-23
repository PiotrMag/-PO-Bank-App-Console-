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
    /// Logika interakcji dla klasy Cards.xaml
    /// </summary>
    public partial class Cards : Page
    {
        public Cards()
        {
            InitializeComponent();
            List<Card> list = PaymentCenter.Instance.GetCards();
            gridView.AutoGenerateColumns = true;
            gridView.ItemsSource = list;
        }

        private void MoveBack(object sender, System.Windows.RoutedEventArgs e)
        {
            var menu = new MainMenu();
            NavigationService.Navigate(menu);
        }
    }
}
