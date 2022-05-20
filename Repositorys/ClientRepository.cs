using Newtonsoft.Json;
using System;
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
        private const string jsonFilePathDB = @"\ClientDB.json";

        Random rnd = new();

        /// <summary>
        /// заполнение базы случаными клиентами
        /// </summary>
        /// <returns></returns>
        public List<Client> InitialDB()
        {
            string[] SurNameArr = new string[5] { "Иванов", "Петров", "Сидоров", "Ковров", "Петросян" };
            string[] NameArr = new string[5] { "Василий", "Андрей", "Петр", "Иван", "Сергей" };
            string[] PatronymicArr = new string[5] { "Николаевич", "Владимирович", "Иванович", "Петрович", "Тимофеевич" };
            List<Client> client = new();
            for (int i = 0; i < 2; i++)
                client.Add(new Client
                {
                    SurName = SurNameArr[rnd.Next(5)],
                    Name = NameArr[rnd.Next(5)],
                    Patronymic = PatronymicArr[rnd.Next(5)],
                    Id = i + 1
                });
            return client;
        }

        /// <summary>
        /// путь к файлу БД клиентов
        /// </summary>
        /// <returns></returns>
        public string GetDBFilePath()
        {
            string dataBase;
            try
            {
                dataBase = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location).TrimEnd('\\') + jsonFilePathDB;
            }
            catch
            {
                dataBase = jsonFilePathDB;
            }
            return dataBase;
        }

        /// <summary>
        /// Сохранение списка клиентов в файл
        /// </summary>
        /// <param name="saveData"></param>
        public void SaveClientData(List<Client> clients)
        {

            if (clients?.Count > 0)
            {
                string filePath = GetDBFilePath();
                FileInfo fi = new(filePath);
                if (!Directory.Exists(fi.DirectoryName)) Directory.CreateDirectory(fi.DirectoryName);
                JsonSerializer jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                using StreamWriter text_writer = File.CreateText(filePath);
                jsonSerializer.Serialize(text_writer, clients);
            }
        }

        /// <summary>
        /// Загрузка списка данных клиентов из json Файла
        /// </summary>
        /// <returns></returns>
        public List<Client> LoadClientData()
        {
            string filePath = GetDBFilePath();
            List<Client> data = new();
            if (File.Exists(filePath))
            {
                JsonSerializer jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                using StreamReader text_reader = File.OpenText(filePath);

                List<Client> Clients = new();
                jsonSerializer.Deserialize(text_reader, typeof(List<Client>));
                if (Clients?.Count > 0)
                {
                    foreach (Client client in Clients)
                    {
                        data.Add(client);
                    }
                }
            }
            else
            {
                SaveClientData(InitialDB());
                data = LoadClientData();
            }
            return data;
        }


        //public Client SelectDepositsByClientId(string id)
        //{
        //    return Clients.FirstOrDefault(e => e.Id == id);
        //}

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
        public ClientRepository()
        {
            LoadClientData();
            SaveClientData(InitialDB());
        }
        #endregion
    }
}
