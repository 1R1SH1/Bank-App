using System.Windows;

namespace Bank_A_WpfApp.TransferBetweenClients
{

    /// <summary>
    /// Логика взаимодействия для TransferToClients.xaml
    /// </summary>
    public partial class TransferToClients : Window
    {
        public TransferToClients(Deposit deposit)
        {
            InitializeComponent();

            this.DataContext = deposit;
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Button_No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
