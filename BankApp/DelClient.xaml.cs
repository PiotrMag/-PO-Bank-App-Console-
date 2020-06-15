﻿using AppLogic;
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
    /// Logika interakcji dla klasy DelClient.xaml
    /// </summary>
    public partial class DelClient : Page
    {
        public DelClient()
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
            bool success = true;
            try
            {
                PaymentCenter.Instance.DeleteClientRequest(number.Text);
            }
            catch(WrongUserException ex)
            {
                success = false;
                MessageBox.Show(ex.Message);
            }
            catch(NotEmptyAccountException ex2)
            {
                success = false;
                MessageBox.Show(ex2.Message + "\r\n" + ex2.Amount);
            }
            if (success)
            {
                MessageBox.Show("Pomyślnie usunięto klienta");
                var mainMenu = new MainMenu();
                NavigationService.Navigate(mainMenu);
            }
        }
    }
}
