using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Bank_A_WpfApp
{
    public class ClientRepository
    {
        private const string jsonFilePathDB = "ClientDB.json";

        public List<Client> clients { get; set; }
        public List<Client> GetAllClients()
        {
            List<Client> client = ReadJson(jsonFilePathDB);
            return client;
        }

        void AddClients()
        {
            string[] NameArr = new string[5] { "Иванов", "Петров", "Сидоров", "Ковров", "Петросян" };
            string[] SurNameArr = new string[5] { "Василий", "Андрей", "Петр", "Иван", "Сергей" };
            string[] PatronymicArr = new string[5] { "Николаевич", "Владимирович", "Иванович", "Петрович", "Тимофеевич" };
            Random rnd = new Random();
            List<Client> clients = new();
            for (int i = 0; i < 1_000; i++)
                clients.Add(new Client
                {
                    Id = rnd.Next(0, 1_000),
                    Name = NameArr[rnd.Next(5)],
                    SurName = SurNameArr[rnd.Next(5)],
                    Patronymic = PatronymicArr[rnd.Next(5)]
                });
            if (!File.Exists(jsonFilePathDB))
            {
                SaveClients(clients);
            }
        }

        public async void AddAllClientsAsync()
        {
            await Task.Run(() => AddClients());
            MessageBox.Show("Все клиенты загружены!", "Загрузка завершена", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public async void SaveClients(List<Client> client)
        {
            await Task.Run(() => WriteJson(jsonFilePathDB, client));
        }

        private void WriteJson(string filePath, List<Client> client)
        {
            string json = JsonConvert.SerializeObject(client);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(json);
            }
        }

        public List<Client> ReadJson(string filePath)
        {
            if (!File.Exists(jsonFilePathDB))
            {
                return new List<Client>();
            }
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadToEnd();

                return JsonConvert.DeserializeObject<List<Client>>(json);
            }
        }
    }
}
