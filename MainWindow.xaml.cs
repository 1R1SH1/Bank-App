using Bank_A_WpfApp.DepositOpenWindow;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bank_A_WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DepositRepository repoDp = new();
        private ClientRepository repoCl = new();

        public Client Clients { get; set; }
        public Deposit Deposits { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            clientList.ItemsSource = repoCl.GetClients();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Банк_А_рянняя версия", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClientInfo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var client = (e.OriginalSource as ListView).SelectedItem as Client;
            int clientId = client.Id;
            depositList.ItemsSource = repoDp.GetDeposits().Where(dep => dep.ClientId == clientId);
        }

        private void ClientList_OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserColumnHeader_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void DepositList_OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void DepositInfo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (depositList.SelectedItems != null)
            {
                List<Deposit> deposit = (e.OriginalSource as ListView).SelectedItems as List<Deposit>;
                infoList.ItemsSource = deposit;
            }
        }

        private void MenuItemMakeTransfer_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Open_Click(object sender, RoutedEventArgs e)
        {
            var deposit = new List<Deposit>();
            OpenDeposit asDeposit = new(deposit as List<Deposit>);
            asDeposit.ShowDialog();
            //int clientId = Deposits.ClientId;
            if (asDeposit.DialogResult.HasValue && asDeposit.DialogResult.Value) ;
            repoDp.GetDeposits()/*.Where(dep => dep.ClientId == clientId)*/;
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
