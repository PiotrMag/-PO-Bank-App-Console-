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
    /// Logika interakcji dla klasy AddCard.xaml
    /// </summary>
    public partial class AddCard : Page
    {
        public AddCard()
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
            Card card = null;
            try
            {
                switch (box.Text)
                {
                    case "Karta bankomatowa":
                        card = PaymentCenter.Instance.AddNewCardRequest(number.Text, Card.CardType.ATMCard, bank.Text);
                        MessageBox.Show("Dodano kartę o numerze: " + card.Number);
                        break;
                    case "Karta debetowa":
                        //dodać monit o max wysokość debetu
                        card = PaymentCenter.Instance.AddNewCardRequest(number.Text, Card.CardType.DebitCard, bank.Text);
                        MessageBox.Show("Dodano kartę o numerze: " + card.Number);
                        break;
                    case "Karta kredytowa":
                        card = PaymentCenter.Instance.AddNewCardRequest(number.Text, Card.CardType.CreditCard, bank.Text);
                        MessageBox.Show("Dodano kartę o numerze: " + card.Number);
                        break;
                    default:
                        MessageBox.Show("Nie wybrano typu karty");
                        break;
                }
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
                        AddUserDialog user = new AddUserDialog(number.Text, Card.CardType.ATMCard, bank.Text);
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
            catch(InactiveBankException ex4)
            {
                MessageBox.Show(ex4.Message);
            }
        }
    }
}
