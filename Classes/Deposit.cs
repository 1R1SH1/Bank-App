using System;
using System.Collections.Generic;

namespace Bank_A_WpfApp
{
    public class Deposit
    {
        #region поля
        /// <summary>
        /// Доступ в репозеторий
        /// </summary>
        private DepositRepository repoDp = new();
        #endregion

        #region свойства
        /// <summary>
        /// Номер счёта
        /// </summary>
        public string DepositNumber { get; set; }

        /// <summary>
        /// Средства на счёте
        /// </summary>
        public int AmountFunds { get; set; }

        /// <summary>
        /// Тип счёта
        /// </summary>
        public string DepositType { get; set; }

        /// <summary>
        /// Индивидуальный идентификатор счёта для привязки к клиентам
        /// </summary>
        public int ClientId { get; set; }
        #endregion

        #region методы
        /// <summary>
        /// Удаляем счёт
        /// </summary>
        public void RemoveDeposit()
        {
            repoDp.GetAllDeposits();

            List<Deposit> deposit = repoDp.GetAllDeposits();

            deposit.RemoveAt(1);

            repoDp.SaveDeposits(deposit);
        }

        /// <summary>
        /// Добавляем счёт
        /// </summary>
        public void AddDeposit()
        {
            Random rnd = new Random();

            List<Deposit> deposit = repoDp.GetAllDeposits();

            string[] DepositTypeArr = new string[2] { "Капитализированный", "Некапитализированный" };

            var deposits = new Deposit()
            {
                DepositNumber = $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}",
                AmountFunds = rnd.Next(1000, 100000),
                DepositType = DepositTypeArr[rnd.Next(2)],
                ClientId = rnd.Next(1, 3)
            };

            deposit.Add(deposits);

            repoDp.SaveDeposits(deposit);
        }
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
