using Bank_A_WpfApp.DepositOpenWindow;
using System.Collections.Generic;
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
        private Repository repo = new();

        public MainWindow()
        {
            InitializeComponent();

            clientList.ItemsSource = repo.InitialDB();
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
            if (clientList.SelectedItems != null)
            {
                List<Deposit> client = (e.OriginalSource as ListView).SelectedItems as List<Deposit>;
                depositList.ItemsSource = client;
            }
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
            List<Deposit> deposit = new();
            OpenDeposit asDeposit = new OpenDeposit(deposit as List<Deposit>);
            asDeposit.ShowDialog();
            if (asDeposit.DialogResult.HasValue && asDeposit.DialogResult.Value)
                repo.OpenDeposit();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
