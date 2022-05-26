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
        public Client selectedClient { get; set; }
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
                List<Deposit> deposit = (e.OriginalSource as ListView).SelectedItems as List<Deposit>;
                infoList.ItemsSource = deposit;
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
        /// Кнопка перевод
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Transfer_Click(object sender, RoutedEventArgs e)
        {
            var deposit = new Deposit();

            selectedClient = clientList.SelectedItem as Client;

            deposit.ClientId = selectedClient.Id;

            repoDp.TransferFunds();

            depositList.ItemsSource = repoDp.GetAllDeposits().Where(dep => dep.ClientId == selectedClient.Id);
        }
    }
}
