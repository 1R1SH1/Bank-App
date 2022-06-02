using System.Collections.Generic;
using System.Windows;

namespace Bank_A_WpfApp.TransferBetweenClients
{

    /// <summary>
    /// Логика взаимодействия для TransferToClients.xaml
    /// </summary>
    public partial class TransferToClients : Window
    {
        /// <summary>
        /// Доступ к репазиторию счетов
        /// </summary>
        private DepositRepository repoDp = new();

        public Deposit Deposits { get; set; }


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

        public void Transfer()
        {
            Deposit sender = new();

            TransferToClients from = new(sender);
            from.ShowDialog();

            int amountTransfer = 1000000;

            Deposit recipient = from.TransferTo.SelectedItem as Deposit;

            if (from.DialogResult.HasValue && from.DialogResult.Value)

                repoDp.TransferFundsClients(sender, recipient, amountTransfer);
        }
    }
}
