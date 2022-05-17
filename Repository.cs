using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Bank_A_WpfApp
{
    public class Repository
    {
        #region поля
        private List<Client> _clients;

        private List<Deposit> _deposits;
        #endregion

        #region свойства
        public List<Client> Clients { get => _clients; set => _clients = value; }
        public List<Deposit> Deposits { get => _deposits; set => _deposits = value; }
        #endregion

        #region методы
        private const string jsonFilePathDB = @"\ClientDB.json";

        private const string jsonFilePathDDB = @"\DepositDB.json";

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
                client.Add(new Client(surName: SurNameArr[rnd.Next(5)],
                                      name: NameArr[rnd.Next(5)],
                                      patronymic: PatronymicArr[rnd.Next(5)],
                                      id: Guid.NewGuid().ToString()));
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
        /// заполнение базы случаными счетами
        /// </summary>
        /// <returns></returns>
        public List<Deposit> InitialDDB()
        {
            string[] DepositTypeArr = new string[2] { "Капитализированный", "Некапитализированный" };
            List<Deposit> deposit = new();
            for (int i = 0; i < 4; i++)
                deposit.Add(new Deposit(depositNumber: $"{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)} {rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}",
                                        amountFunds: $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                     $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                     $"{rnd.Next(10)}{rnd.Next(10)}",
                                        depositType: DepositTypeArr[rnd.Next(2)]));
            return deposit;
        }

        /// <summary>
        /// путь к файлу БД счетов
        /// </summary>
        /// <returns></returns>
        public string GetDDBFilePath()
        {
            string dataBase;
            try
            {
                dataBase = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location).TrimEnd('\\') + jsonFilePathDDB;
            }
            catch
            {
                dataBase = jsonFilePathDDB;
            }
            return dataBase;
        }

        /// <summary>
        /// Сохранение коллекции данных счетов в файл
        /// </summary>
        /// <param name="savedData"></param>
        public void SaveDepositData(List<Deposit> Deposits)
        {
            if (Deposits?.Count > 0)
            {
                string filePath = GetDDBFilePath();
                FileInfo fI = new(filePath);
                if (!Directory.Exists(fI.DirectoryName)) Directory.CreateDirectory(fI.DirectoryName);
                JsonSerializer jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                using StreamWriter text_writer = File.CreateText(filePath);
                jsonSerializer.Serialize(text_writer, Deposits);
            }
        }

        /// <summary>
        /// Загрузка списка данных счетов из json Файла
        /// </summary>
        /// <returns></returns>
        public List<Deposit> LoadDepositData()
        {
            string filePath = GetDDBFilePath();
            List<Deposit> data = new();
            if (File.Exists(filePath))
            {
                JsonSerializer jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                using StreamReader text_reader = File.OpenText(filePath);

                List<Deposit> deposits = new();
                jsonSerializer.Deserialize(text_reader, typeof(List<Deposit>));
                if (deposits?.Count > 0)
                {
                    foreach (Deposit deposit in deposits)
                    {
                        data.Add(deposit);
                    }
                }
            }
            else
            {
                SaveDepositData(InitialDDB());
                data = LoadDepositData();
            }
            return data;
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

        /// <summary>
        /// Открываем счёт
        /// </summary>
        /// <param name="client"></param>
        public void OpenDeposit()
        {
            string[] DepositTypeArr = new string[2] { "Капитализированный", "Некапитализированный" };
            List<Deposit> deposit = new();
            deposit.Add(new Deposit(depositNumber: $"{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)} {rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                      $"{rnd.Next(10)}",
                                        amountFunds: $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                     $"{rnd.Next(10)}{rnd.Next(10)}" +
                                                     $"{rnd.Next(10)}{rnd.Next(10)}",
                                        depositType: DepositTypeArr[rnd.Next(2)]));
        }
        #endregion

        #region конструкторы
        /// <summary>
        /// Репозиторий
        /// </summary>
        /// <param name="clientType">тип отображения консультант или менеджер</param>
        public Repository()
        {
            LoadClientData();
            LoadDepositData();
        }
        #endregion
    }
}
