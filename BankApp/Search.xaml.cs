using AppLogic;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
                List<ArchiveRecord> records = PaymentCenter.Instance.SearchArchives(query);
                gridView.AutoGenerateColumns = true;
                gridView.ItemsSource = records;
            }
            catch (DBNotBoundException e)
            {
                MessageBox.Show(e.Message + "\r\nŚcieżka: " + e.DBFilePath);
                return;
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message + "\r\nKod błędu: " + ex.SqliteExtendedErrorCode);
                return;
            }
            catch(SQLiteException ex2)
            {
                MessageBox.Show(ex2.Message);
                return;
            }
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            var database = new Database();
            NavigationService.Navigate(database);
        }
    }
}
