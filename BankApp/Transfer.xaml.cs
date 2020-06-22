using AppLogic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy Transfer.xaml
    /// </summary>
    public partial class Transfer : Page
    {
        public Transfer()
        {
            InitializeComponent();
        }

        private void DoTransfer(object sender, RoutedEventArgs e)
        {
            decimal Amount = 0;
            try
            {
                bool success = decimal.TryParse(amount.Text, out Amount);
                if (!success) throw new WrongSumException("Podano błędną wartość w polu \"Kwota przelewu\"");
                PaymentCenter.Instance.MakeTransactionRequest(fromCard.Text, toCard.Text, Amount);
                PaymentCenter.Instance.PrepareArchiveLog(Amount, BankActionResult.SUCCESS, fromCard.Text, toCard.Text);
            }
            catch (WrongSumException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (TransactionDeniedException ex2)
            {
                MessageBox.Show(ex2.Message);
                PaymentCenter.Instance.PrepareArchiveLog(Amount, BankActionResult.TRANSACTION_REJECTED, fromCard.Text, toCard.Text);
            }
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction();
            NavigationService.Navigate(transaction);
        }
    }
}
