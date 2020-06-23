using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BankApp
{
    /// <summary>
    /// Logika interakcji dla klasy Database.xaml
    /// </summary>
    public partial class Database : Page
    {
        public Database()
        {
            InitializeComponent();
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            var mainMenu = new MainMenu();
            NavigationService.Navigate(mainMenu);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            var search = new Search(box.Text);
            NavigationService.Navigate(search);
        }
    }
}
