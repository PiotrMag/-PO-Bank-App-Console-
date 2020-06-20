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
using System.Windows.Shapes;
using static AppLogic.Card;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy DebitHeight.xaml
    /// </summary>
    public partial class DebitHeight : Window
    {
        public string ClientNumber;
        public string BankName;
        internal DebitHeight(string clientNumber, string bankName)
        {
            InitializeComponent();
            ClientNumber = clientNumber;
            BankName = bankName;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            Card card = null;
            try
            {
                card = PaymentCenter.Instance.AddNewCardRequest(ClientNumber, CardType.CreditCard, BankName, decimal.Parse(debit.Text));
                success = true;
            }
            catch (NullUserException ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (WrongUserException ex2)
            {
                MessageBoxResult result = MessageBox.Show(ex2.Message, "Word Processor", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        AddUserDialog user = new AddUserDialog(ClientNumber, CardType.CreditCard, BankName);
                        user.ShowDialog();
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }
            catch (NoSuchBankException ex3)
            {
                MessageBox.Show(ex3.Message + "\r\n" + ex3.Name);
            }
            catch (InactiveBankException ex4)
            {
                MessageBox.Show(ex4.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Coś poszło nie tak.\r\nKarta nie została dodana.");
            }
            if (success) MessageBox.Show("Pomyślnie dodano kartę o numerze: " + card.Number);
            Close();
        }
    }
}
