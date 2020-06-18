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
using AppLogic;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddBank.xaml
    /// </summary>
    public partial class AddBank : Page
    {
        public AddBank()
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
            try
            {
                PaymentCenter.Instance.AddBank(name.Text);
                MessageBox.Show("Pomyslnie dodano bank o nazwie: " + name.Text);
            }
            catch(BankAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
