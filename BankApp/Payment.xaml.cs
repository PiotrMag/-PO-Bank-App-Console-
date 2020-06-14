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
    /// Logika interakcji dla klasy Payment.xaml
    /// </summary>
    public partial class Payment : Page
    {
        public Payment()
        {
            InitializeComponent();
        }

        private void DoPayment(object sender, RoutedEventArgs e)
        {
            double Amount=0;
            try
            {
                bool success = double.TryParse(amount.Text, out Amount);
                if (!success) throw new WrongSumException("Podano błędną wartość w polu \"Kwota przelewu\"");
                PaymentCenter.Instance.OneCardTransactionRequest(card.Text, Amount);
                PaymentCenter.Instance.PrepareArchiveLog(Amount, BankActionResult.SUCCESS, null, card.Text);
            }
            catch (WrongSumException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NoSuchCardException ex2)
            {
                MessageBox.Show(ex2.Message);
                PaymentCenter.Instance.PrepareArchiveLog(Amount, BankActionResult.REJECTED_NO_SUCH_USER, null, card.Text);
            }
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction();
            NavigationService.Navigate(transaction);
        }
    }
}
