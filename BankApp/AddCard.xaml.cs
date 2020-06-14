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
            try
            {
                switch (box.Text)
                {
                    case "Karta bankomatowa":
                        PaymentCenter.Instance.AddNewCardRequest(number.Text, Card.CardType.ATMCard, bank.Text);
                        break;
                    case "Karta debetowa":
                        PaymentCenter.Instance.AddNewCardRequest(number.Text, Card.CardType.DebitCard, bank.Text);
                        break;
                    case "Karta kredytowa":
                        PaymentCenter.Instance.AddNewCardRequest(number.Text, Card.CardType.CreditCard, bank.Text);
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
                MessageBox.Show(ex2.Message);
            }
            catch (NoSuchBankException ex3)
            {
                MessageBox.Show(ex3.Message + "\r\n" + ex3.Name);
            }
        }
    }
}
