using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bank_A_WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Доступ к репазиторию счетов
        /// </summary>
        private DepositRepository repoDp = new();

        /// <summary>
        /// Доступ к репазиторию клиентов
        /// </summary>
        private ClientRepository repoCl = new();

        /// <summary>
        /// Счёт
        /// </summary>
        private Deposit Deposits = new();

        /// <summary>
        /// Клиент
        /// </summary>
        public Client selectedClient = new();

        public MainWindow()
        {
            InitializeComponent();
            //Выводим всех клиентов в интерфейс
            clientList.ItemsSource = repoCl.GetAllClients();
        }

        /// <summary>
        /// Информация о версии программы Банк_А
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Банк_А_рянняя версия", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Кнопка выход в меню Файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Выбираем клиента в интерфейсе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientInfo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var client = (e.OriginalSource as ListView).SelectedItem as Client;
            int clientId = client.Id;
            depositList.ItemsSource = repoDp.GetAllDeposits().Where(dep => dep.ClientId == clientId);
        }

        /// <summary>
        /// Выбираем счёт в интерфейсе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepositInfo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (depositList.SelectedItems != null)
            {
                Deposit deposit = (e.OriginalSource as ListView).SelectedItems as Deposit;
                infoList.ItemsSource = (System.Collections.IEnumerable)deposit;
            }
        }

        /// <summary>
        /// Кнопка открыть счёт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Open_Click(object sender, RoutedEventArgs e)
        {
            var deposit = new Deposit();

            selectedClient = clientList.SelectedItem as Client;

            deposit.ClientId = selectedClient.Id;

            Deposits?.AddDeposit();

            depositList.ItemsSource = repoDp.GetAllDeposits().Where(dep => dep.ClientId == selectedClient.Id);
        }

        /// <summary>
        /// Кнопка закрыть счёт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            var deposit = new Deposit();

            selectedClient = clientList.SelectedItem as Client;

            deposit.ClientId = selectedClient.Id;

            Deposits?.RemoveDeposit();

            depositList.ItemsSource = repoDp.GetAllDeposits().Where(dep => dep.ClientId == selectedClient.Id);
        }

        /// <summary>
        /// Кнопка перевода между счетами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Transfer_Deposits_Click(object sender, RoutedEventArgs e)
        {
            var deposit = new Deposit();

            selectedClient = clientList.SelectedItem as Client;

            deposit.ClientId = selectedClient.Id;

            repoDp.TransferFundsDeposits();

            depositList.ItemsSource = repoDp.GetAllDeposits().Where(d => d.ClientId == selectedClient.Id);
        }

        /// <summary>
        /// Кнопка перевода между клиентами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Transfer_Clients_Click(object sender, RoutedEventArgs e)
        {

            List<Deposit> senders = depositList.SelectedItem as List<Deposit>;

            List<Deposit> recipient = transferTo.SelectedItem as List<Deposit>;

            bool result = Int32.TryParse(amountTransferTextBox.Text, out int amountTransfer);

            repoDp.TransferFundsClients(senders, recipient, amountTransfer);

            pTransfer.IsOpen = false;

            depositList.ItemsSource = repoDp.GetAllDeposits().Where(d => d.ClientId == selectedClient.Id);
        }

        /// <summary>
        /// Кнопка пополнения счёта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_AddFunds_Clients_Click(object sender, RoutedEventArgs e)
        {
            var deposit = new Deposit();

            selectedClient = clientList.SelectedItem as Client;

            deposit.ClientId = selectedClient.Id;

            repoDp.AddFunds();

            depositList.ItemsSource = repoDp.GetAllDeposits().Where(d => d.ClientId == selectedClient.Id);
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
            transferTo.ItemsSource = clientList.ItemsSource;
        }
    }
}
