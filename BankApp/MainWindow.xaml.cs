using AppLogic;
using Microsoft.Data.Sqlite;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;

namespace BankApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {

        static public string SystemStatePath = "system_state.xml";
        static public string DBPath = "archive.db";
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                //wczytywanie bazy danych
                PaymentCenter.Instance.InitDB(DBPath);
                PaymentCenter.Instance.LoadSystemState(SystemStatePath);

            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message + "\r\nKod błędu: " + ex.SqliteExtendedErrorCode);
            }
            catch (NoSuchFileException nsfex)
            {
                MessageBox.Show(nsfex.Message + "\r\nNie udało się załadować stanu systemu z pliku: " + nsfex.FilePath + "\r\nUżywanie czystego(nowego) stanu systemu");
            }
        }

        private void Quit(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy chcesz zapisać zmiany przed zamknięciem?", "", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
                case MessageBoxResult.Yes:
                    if (!PaymentCenter.Instance.SaveSystemState(MainWindow.SystemStatePath))
                        MessageBox.Show("Wystąpił błąd zapisu");
                    else
                        Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    Application.Current.Shutdown();
                    break;
            }
        }
    }
}
