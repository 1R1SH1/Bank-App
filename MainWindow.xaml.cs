﻿using Bank_A_WpfApp.Classes;
using Saving_InfoLog_ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NullReferenceException = Bank_A_WpfApp.Classes.NullReferenceException;

namespace Bank_A_WpfApp
{
    public partial class MainWindow : Window
    {
        private Random _random = new Random();
        private DepositRepository _depositRepository = new();
        private ClientRepository _clientRepository = new();
        private InfoLog _log = new();
        private SavingMethod _savingInfoLogs = new();

        public event Action<string> Transaction;
        public List<Deposit> deposits { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Transaction += LogRepository_Transaction;
            _clientRepository.AddAllClientsAsync();
            Thread.Sleep(3500);
            clientList.ItemsSource = _clientRepository.GetAllClients();
            infoList.ItemsSource = _log.log;
            clientList.Items.Refresh();
        }

        private void LogRepository_Transaction(string message)
        {
            _log.AddToLog(message);
            infoList.Items.Refresh();
            _savingInfoLogs.SaveInfoLog(_log.log);
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Банк_А_Версия_2.0", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClientInfo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var client = (e.OriginalSource as ListView).SelectedItem as Client;
            int clientId = client.Id;
            depositList.ItemsSource = _depositRepository.GetAllDeposits().Where(dep => dep.ClientId == clientId);
        }

        private void Button_Open_Click(object sender, RoutedEventArgs e)
        {
            var selectedClient = clientList.SelectedItem as Client;

            try
            {
                if (selectedClient == null)
                {
                    throw new NullReferenceException("Клиент не выбран");
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var deposit = new Deposit
            {
                ClientId = selectedClient.Id,
                AmountFunds = _random.Next(10000),
                DepositNumber = GenerateDepositNumber(),
                DepositType = "Не капитализированный"
            };

            var allDeposits = _depositRepository.GetAllDeposits();
            allDeposits.Add(deposit);
            _depositRepository.SaveDeposits(allDeposits);

            depositList.ItemsSource = _depositRepository.GetAllDeposits().Where(dep => dep.ClientId == selectedClient.Id);

            Transaction?.Invoke($"Открыт счёт клиенту ${selectedClient.Name}");
        }

        private async void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            var selectedClient = clientList.SelectedItem as Client;
            var selectedDeposit = depositList.SelectedItem as Deposit;

            try
            {
                if (selectedDeposit == null)
                {
                    throw new NullReferenceException("Счёт не выбран");
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _depositRepository.RemoveDepositByDepositNumber(selectedDeposit.DepositNumber);

            depositList.ItemsSource = _depositRepository.GetAllDeposits().Where(dep => dep.ClientId == selectedClient.Id);
            Transaction?.Invoke($"Счёт {selectedDeposit.DepositNumber} закрыт");
        }

        private async void Button_Transfer_Clients_Click(object sender, RoutedEventArgs e)
        {
            Deposit senders = depositList.SelectedItem as Deposit;
            Client client = transferToClient.SelectedItem as Client;
            Deposit recipient = transferToDeposit.SelectedItem as Deposit;

            bool result = Int32.TryParse(amountTransferTextBox.Text, out int amountTransfer);
            if (!result)
            {
                MessageBox.Show("Неправильно введена сумма", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool checkFunds = CheckFundsAmount(senders, Int32.Parse(amountTransferTextBox.Text));
            if (!checkFunds)
            {
                MessageBox.Show("Недостаточно средств", "Недостаточно средств", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            TransferBetweenClients(senders, recipient, amountTransfer);

            depositList.Items.Refresh();

            pTransfer.IsOpen = false;

            MessageBox.Show("Успешно", "Перевод совершён", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void TransferBetweenClients(Deposit sender, Deposit recipient, int amount)
        {
            sender.DeductFunds(amount);
            recipient.AddFunds(amount);

            _depositRepository.Update(sender);
            _depositRepository.Update(recipient);

            Transaction?.Invoke($"Переведено ${amount} со счёта {sender.DepositNumber} на счёт {recipient.DepositNumber}");
        }

        private async void Button_AddFunds_Clients_Click(object sender, RoutedEventArgs e)
        {
            Deposit recipient = depositList.SelectedItem as Deposit;
            Client client = addFundsToClient.SelectedItem as Client;

            recipient.ClientId = client.Id;

            bool result = Int32.TryParse(amountFundsTextBox.Text, out int amountTransfer);
            if (!result)
            {
                MessageBox.Show("Успешно", "Счёт пополнен", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            AddFundsClients(recipient, amountTransfer);

            pAddFunds.IsOpen = false;

            depositList.Items.Refresh();

            MessageBox.Show("Успешно", "Счёт пополнен", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void AddFundsClients(Deposit recipient, int amount)
        {
            recipient.AddFunds(amount);

            _depositRepository.Update(recipient);

            Transaction?.Invoke($"Счёт {recipient.DepositNumber} пополнен на сумму ${amount}");
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

        private void MenuItemAddFunds_OnClick(object sender, RoutedEventArgs e)
        {
            pAddFunds.IsOpen = true;
            addFundsToClient.ItemsSource = clientList.ItemsSource;
            addFundsToDeposit.ItemsSource = depositList.ItemsSource;
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

        private void transferToClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipient = transferToClient.SelectedItem as Client;
            transferToDeposit.ItemsSource = _depositRepository.GetAllDeposits().Where(d => d.ClientId == selectedRecipient.Id);
        }

        private void addFundsToClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipient = addFundsToClient.SelectedItem as Client;
            addFundsToDeposit.ItemsSource = _depositRepository.GetAllDeposits().Where(d => d.ClientId == selectedRecipient.Id);
        }

        public bool CheckFundsAmount(Deposit deposit, int amount)
        {
            bool result = deposit.AmountFunds >= amount;
            return result;
        }

        private void MenuItem_Refresh(object sender, RoutedEventArgs e)
        {
            clientList.Items.Refresh();
        }
    }
}
