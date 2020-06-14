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
    /// Logika interakcji dla klasy Withdraw.xaml
    /// </summary>
    public partial class Withdraw : Page
    {
        public Withdraw()
        {
            InitializeComponent();
        }

        private void DoWithdraw(object sender, RoutedEventArgs e)
        {
            try
            {
                bool success = double.TryParse(amount.Text, out double Amount);
                if (!success) throw new WrongSumException("Podano błędną wartość w polu \"Kwota przelewu\"");
                PaymentCenter.Instance.OneCardTransactionRequest(card.Text, (-1)*Amount);
                //Log in archive
            }
            catch (WrongSumException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(NoSuchCardException ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction();
            NavigationService.Navigate(transaction);
        }
    }
}
