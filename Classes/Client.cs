using System.Collections.Generic;

namespace Bank_A_WpfApp
{
    public class Client
    {
        #region поля
        private string _id;

        private string _surName;

        private string _name;

        private string _patronymic;
        #endregion

        #region свойства
        public string Id { get => _id; set => _id = value; }

        public string SurName { get => _surName; set => _surName = value; }

        public string Name { get => _name; set => _name = value; }

        public string Patronymic { get => _patronymic; set => _patronymic = value; }

        public List<Deposit> Deposits { get; set; }
        #endregion

        #region методы

        #endregion

        #region конструкторы
        public Client(string id, string surName, string name, string patronymic)
        {
            _id = id;
            _surName = surName;
            _name = name;
            _patronymic = patronymic;
        }
        #endregion
    }
}
