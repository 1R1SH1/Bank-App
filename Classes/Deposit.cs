namespace Bank_A_WpfApp
{
    public class Deposit
    {
        #region поля

        private string _depositNumber;

        private string _amountFunds;

        private string _depositType;
        #endregion

        #region свойства
        public string DepositNumber { get => _depositNumber; set => _depositNumber = value; }

        public string AmountFunds { get => _amountFunds; set => _amountFunds = value; }

        public string DepositType { get => _depositType; set => _depositType = value; }
        #endregion

        #region методы

        #endregion

        #region конструкторы
        public Deposit(string depositNumber, string amountFunds, string depositType)
        {
            _depositNumber = depositNumber;
            _amountFunds = amountFunds;
            _depositType = depositType;
        }
        #endregion
    }
}
