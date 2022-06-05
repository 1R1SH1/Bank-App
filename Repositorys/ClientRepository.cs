using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bank_A_WpfApp
{
    public class ClientRepository
    {
        private const string jsonFilePathDB = "ClientDB.json";

        public List<Client> GetAllClients()
        {
            if (!File.Exists(jsonFilePathDB))
            {
                return new List<Client>()
                {
                    new Client{Id = 1, Name = "Игорь", SurName = "Петров", Patronymic = "Игоревич"},
                    new Client{Id = 2, Name = "Фин", SurName = "Флин", Patronymic = "Флинович"},
                    new Client{Id = 3, Name = "Джек", SurName = "Джеков", Patronymic = "Джекович"}
                };
            }
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
    }
}
