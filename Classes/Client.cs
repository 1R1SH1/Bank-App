using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Bank_A_WpfApp
{
    public class Client
    {
        #region поля

        #endregion

        #region свойства
        /// <summary>
        /// Индивидуальный идентификатор клиента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string SurName { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество клиента
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Список счетов
        /// </summary>
        public List<Deposit> Deposits { get; set; }
        #endregion

        #region методы

        #endregion

        #region конструкторы
        /// <summary>
        /// Клиенты со счетами
        /// </summary>
        public Client()
        {
            Deposits = new List<Deposit>();
        }
        #endregion
    }
}
