using System.Collections.Generic;

namespace Bank_A_WpfApp
{
    public class Client
    {
        #region поля

        #endregion

        #region свойства
        public int Id { get; set; }

        public string SurName { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public List<Deposit> Deposits { get; set; }

        public List<Client> Clients { get; set; }
        #endregion

        #region методы

        #endregion

        #region конструкторы
        public Client()
        {
            Deposits = new List<Deposit>();
        }
        #endregion
    }
}
