using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bank_A_WpfApp
{
    public partial class MainWindow : Window
    {
        private Random _random = new Random();
        private DepositRepository _depositRepository = new();
        private ClientRepository _clientRepository = new();

        public MainWindow()
        {
            InitializeComponent();
            //Выводим всех клиентов в интерфейс
            clientList.ItemsSource = _clientRepository.GetAllClients();
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
            depositList.ItemsSource = _depositRepository.GetAllDeposits().Where(dep => dep.ClientId == clientId);
        }

        private void DepositInfo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (depositList.SelectedItems != null)
            {
                Deposit deposit = (e.OriginalSource as ListView).SelectedItems as Deposit;
                infoList.ItemsSource = (System.Collections.IEnumerable)deposit;
            }
        }

        private void Button_Open_Click(object sender, RoutedEventArgs e)
        {
            var selectedClient = clientList.SelectedItem as Client;

            var deposit = new Deposit
            {
                ClientId = selectedClient.Id,
                AmountFunds = _random.Next(10000),
                DepositNumber = GenerateDepositNumber(),
                DepositType = "Расчётный"
            };

            var allDeposits = _depositRepository.GetAllDeposits();
            allDeposits.Add(deposit);
            _depositRepository.SaveDeposits(allDeposits);


            depositList.ItemsSource = _depositRepository.GetAllDeposits().Where(dep => dep.ClientId == selectedClient.Id);
        }
                
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            var selectedClient = clientList.SelectedItem as Client;
            var selectedDeposit = depositList.SelectedItem as Deposit;

            _depositRepository.RemoveDepositByDepositNumber(selectedDeposit.DepositNumber);


            depositList.ItemsSource = _depositRepository.GetAllDeposits().Where(dep => dep.ClientId == selectedClient.Id);
        }

        private void Button_Transfer_Deposits_Click(object sender, RoutedEventArgs e)
        {
            var deposit = new Deposit();

            var selectedClient = clientList.SelectedItem as Client;

            deposit.ClientId = selectedClient.Id;


            depositList.ItemsSource = _depositRepository.GetAllDeposits().Where(d => d.ClientId == selectedClient.Id);
        }

        private void Button_Transfer_Clients_Click(object sender, RoutedEventArgs e)
        {                        
            var clientRecipient = transferToClient.SelectedItem as Client;

            var recipient = transferToDeposit.SelectedItem as Deposit;

            TransferBetweenClients();

            pTransfer.IsOpen = false;

            depositList.ItemsSource = _depositRepository.GetAllDeposits();
        }

        private void TransferBetweenClients()
        {
            int amountTransfer = 1000;

            Deposit sender = depositList.SelectedItem as Deposit;

            Deposit recipient = depositList.SelectedItem as Deposit;

            sender.AmountFunds -= amountTransfer;

            recipient.AmountFunds += amountTransfer;
        }

        private void Button_AddFunds_Clients_Click(object sender, RoutedEventArgs e)
        {
            var deposit = new Deposit();

            var selectedClient = clientList.SelectedItem as Client;

            deposit.ClientId = selectedClient.Id;


            depositList.ItemsSource = _depositRepository.GetAllDeposits().Where(d => d.ClientId == selectedClient.Id);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClientList_OnPreviewMouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                ContextMenu cm = this.FindResource("CmButton") as ContextMenu;
                cm.PlacementTarget = sender as Button;
                cm.IsOpen = true;
            }
        }

        private void MenuItemTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            pTransfer.IsOpen = true;
            transferToClient.ItemsSource = clientList.ItemsSource;
            transferToDeposit.ItemsSource = depositList.ItemsSource;
        }

        private string GenerateDepositNumber()
        {
            int[] fours = new int[]
            {
                _random.Next(1000, 9999),
                _random.Next(1000, 9999),
                _random.Next(1000, 9999),
                _random.Next(1000, 9999)
            };
            return String.Join(" ", fours);
        }
    }
}
