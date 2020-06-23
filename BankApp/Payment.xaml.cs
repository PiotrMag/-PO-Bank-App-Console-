using AppLogic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            decimal Amount =0;
            try
            {
                bool success = decimal.TryParse(amount.Text, out Amount);
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
