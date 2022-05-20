using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bank_A_WpfApp
{
    public class ClientRepository
    {
        #region поля

        #endregion

        #region свойства
        public List<Client> Clients { get; set; }

        public static List<Client> _client;
        #endregion

        #region методы
        private const string jsonFilePathDB = "ClientDB.json";

        public List<Client> GetClients()
        {
            string json = File.ReadAllText(jsonFilePathDB);
            return JsonConvert.DeserializeObject<List<Client>>(json);
        }

        public List<Client> SelectDepositsByClientId(int id)
        {
            Clients = new List<Client>();

            for (int i = 0; i < _client.Count; i++)
            {
                if (_client[i].Id == id)
                {
                    Clients.Add(_client[i]);
                }
            }
            return Clients;
        }
        #endregion

        #region конструкторы
        /// <summary>
        /// Репозиторий
        /// </summary>
        /// <param name="clientType">тип отображения консультант или менеджер</param>
        //public ClientRepository()
        //{
        //    Clients = new();
        //}
        #endregion
    }
}
