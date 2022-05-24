using System.Collections.Generic;

namespace Bank_A_WpfApp
{
    public class Deposit
    {
        private object depositNumber;
        #region поля

        #endregion

        #region свойства
        public string DepositNumber { get; set; }
        public int AmountFunds { get; set; }
        public string DepositType { get; set; }
        public int ClientId { get; set; }
        #endregion

        #region методы

        #endregion

        #region конструкторы
        //public Deposit(string depositNumber, int amountFunds, string depositType, int clientId)
        //{
        //    this.DepositNumber = depositNumber;
        //    this.AmountFunds = amountFunds;
        //    this.DepositType = depositType;
        //    this.ClientId = clientId;
        //}
        #endregion
    }
}
