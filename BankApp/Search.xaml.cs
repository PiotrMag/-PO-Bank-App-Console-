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
using Microsoft.Data.Sqlite;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        public Search(string query)
        {
            InitializeComponent();
            try
            {
                PaymentCenter.Instance.SearchArchives(query);
            }
            catch (DBNotBound e)
            {
                MessageBox.Show(e.Message + "\r\nŚcieżka: " + e.DBFilePath);
                return;
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message + "\r\nKod błędu: " + ex.SqliteExtendedErrorCode);
                return;
            }
            //wyświetlanie wyników
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            var database = new Database();
            NavigationService.Navigate(database);
        }
    }
}
