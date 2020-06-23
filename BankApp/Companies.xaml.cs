using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using AppLogic;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy Companies.xaml
    /// </summary>
    public partial class Companies : Page
    {
        public Companies()
        {
            InitializeComponent();
            List<Company> list = PaymentCenter.Instance.GetCompanies();
            gridView.AutoGenerateColumns = true;
            gridView.ItemsSource = list;
        }

        private void MoveBack(object sender, System.Windows.RoutedEventArgs e)
        {
            var menu = new MainMenu();
            NavigationService.Navigate(menu);
        }
    }
}
