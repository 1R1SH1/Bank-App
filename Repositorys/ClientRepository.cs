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
            List<Client> client = ReadJson(jsonFilePathDB);
            return client;
        }

        public List<Client> ReadJson(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadToEnd();

                return JsonConvert.DeserializeObject<List<Client>>(json);
            }
        }
        #endregion

        #region конструкторы
        #endregion
    }
}
