using System.Collections.Generic;
using System.Windows;

namespace Bank_A_WpfApp.DepositOpenWindow
{
    /// <summary>
    /// Логика взаимодействия для OpenDeposit.xaml
    /// </summary>
    public partial class OpenDeposit : Window
    {

        public OpenDeposit(List<Deposit> deposit)
        {
            InitializeComponent();
            this.DataContext = deposit;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
