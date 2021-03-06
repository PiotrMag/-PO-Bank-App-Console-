﻿using AppLogic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            decimal Amount = 0;
            try
            {
                bool success = decimal.TryParse(amount.Text, out Amount);
                if (!success) throw new WrongSumException("Podano błędną wartość w polu \"Kwota przelewu\"");
                PaymentCenter.Instance.OneCardTransactionRequest(card.Text, (-1)*Amount);
                PaymentCenter.Instance.PrepareArchiveLog(Amount, BankActionResult.SUCCESS, card.Text);
            }
            catch (WrongSumException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(InsufficientCardBalanceException ex2)
            {
                MessageBox.Show(ex2.Message + "\r\n" + ex2.Amount);
                PaymentCenter.Instance.PrepareArchiveLog(Amount, BankActionResult.REJECTED_INSUFFICIENT_ACCOUNT_BALANCE, card.Text);
            }
            catch(NoSuchCardException ex3)
            {
                MessageBox.Show(ex3.Message);
                PaymentCenter.Instance.PrepareArchiveLog(Amount, BankActionResult.REJECTED_NO_SUCH_USER, card.Text);
            }
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction();
            NavigationService.Navigate(transaction);
        }
    }
}
