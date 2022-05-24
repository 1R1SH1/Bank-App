using Bank_A_WpfApp.DepositOpenWindow;
using System;
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
        
        public Client selectedClient { get; set; }
        public List<Deposit> DepositsList { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            clientList.ItemsSource = repoCl.GetClients();
        }

        private void PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
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

        private void Button_Open_Click(object sender, RoutedEventArgs e)
        {
            var deposit = new Deposit();
            selectedClient = clientList.SelectedItem as Client;
            deposit.ClientId = selectedClient.Id;

                repoDp?.OpenDeposit();

            depositList.ItemsSource = repoDp.GetDeposits().Where(dep => dep.ClientId == selectedClient.Id);
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            var deposit = new Deposit();
            deposit.ClientId = selectedClient.Id;
            repoDp?.CloseDeposit();
            depositList.ItemsSource = repoDp.GetDeposits().Where(dep => dep.ClientId == selectedClient.Id);
        }

        private void Button_Transfer_Click(object sender, RoutedEventArgs e)
        {
            List<Deposit> currentClient = new List<Deposit>();
            List<Deposit> recipient = new List<Deposit>();

            repoDp.TransferFunds(currentClient, recipient);
            depositList.ItemsSource = repoDp.GetDeposits().Where(dep => dep.ClientId == selectedClient.Id);
        }
    }
}
