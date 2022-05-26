using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bank_A_WpfApp
{
    public class DepositRepository
    {
        #region поля
        /// <summary>
        /// Файл Базы данных счетов
        /// </summary>
        private const string jsonFilePathDDB = "DepositDB.json";
        #endregion

        #region свойства
        #endregion

        #region методы

        /// <summary>
        /// Получаем все счета
        /// </summary>
        /// <returns></returns>
        public List<Deposit> GetAllDeposits()
        {
            List<Deposit> deposit = ReadJson(jsonFilePathDDB);
            return deposit;
        }

        /// <summary>
        /// Сохраняем
        /// </summary>
        /// <param name="deposit"></param>
        public void SaveDeposits(List<Deposit> deposit)
        {
            WriteJson(jsonFilePathDDB, deposit);
        }

        /// <summary>
        /// Метод сохранения в json файл
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="deposit"></param>
        private void WriteJson(string filePath, List<Deposit> deposit)
        {
            string json = JsonConvert.SerializeObject(deposit);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(json);
            }
        }

        /// <summary>
        /// Метод считывания из json файла
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private List<Deposit> ReadJson(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadToEnd();

                return JsonConvert.DeserializeObject<List<Deposit>>(json);
            }
        }

        /// <summary>
        /// Перевод между счетами 1 клиента
        /// </summary>
        public void TransferFunds()
        {
            Random rnd = new();

            int amount = rnd.Next(1000, 10000);

            List<Deposit> deposit = GetAllDeposits();

            List<Deposit> depositsClient1 = deposit.Where(dep => dep.ClientId == 1).ToList();

            List<Deposit> depositsClient2 = deposit.Where(dep => dep.ClientId == 1).ToList();

            List<Deposit> depositsClient3 = deposit.Where(dep => dep.ClientId == 3).ToList();

            List<Deposit> depositsClient4 = deposit.Where(dep => dep.ClientId == 4).ToList();

            depositsClient1[0].AmountFunds -= amount;
            depositsClient2[1].AmountFunds += amount;


            SaveDeposits(depositsClient1);

            SaveDeposits(depositsClient2);

            SaveDeposits(deposit);
        }

        #endregion

        #region конструкторы

        #endregion
    }
}
