using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bank_A_WpfApp
{
    public class ClientRepository
    {
        #region поля
        /// <summary>
        /// Файл Базы данных клиентов
        /// </summary>
        private const string jsonFilePathDB = "ClientDB.json";
        #endregion

        #region свойства
        #endregion

        #region методы
        /// <summary>
        /// Получаем всех клиентов из Базы данных
        /// </summary>
        /// <returns></returns>
        public List<Client> GetAllClients()
        {
            List<Client> client = ReadJson(jsonFilePathDB);
            return client;
        }

        /// <summary>
        /// Мето считывания из json файла
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
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
