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

        #endregion

        #region свойства
        Random rnd = new Random();
        public List<Deposit> Deposits { get; set; }
        public Client Clients { get; set; }
        public Deposit _deposit { get; set; }
        #endregion

        #region методы

        public string jsonFilePathDDB = "DepositDB.json";

        public List<Deposit> GetDeposits()
        {
            List<Deposit> deposits = ReadJson(jsonFilePathDDB);
            return deposits;
        }

        public void WriteJson(string filePath, List<Deposit> deposit)
        {
            string json = JsonConvert.SerializeObject(deposit);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(json);
            }
        }

        public List<Deposit> ReadJson(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadToEnd();

                return JsonConvert.DeserializeObject<List<Deposit>>(json);
            }
        }

        public void OpenDeposit()
        {
            List<Deposit> deposit = ReadJson(jsonFilePathDDB);

            string[] DepositTypeArr = new string[2] { "Капитализированный", "Некапитализированный" };

            var deposits = new Deposit()
            {
                DepositNumber = $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}" +
                                $"{rnd.Next(10)}{rnd.Next(10)}",
                AmountFunds = rnd.Next(1000, 100000),
                DepositType = DepositTypeArr[rnd.Next(2)],
                ClientId = rnd.Next(1, 2)
            };

            deposit.Add(deposits);

            WriteJson(jsonFilePathDDB, deposit);
        }

        public void CloseDeposit()
        {
            List<Deposit> deposit = ReadJson(jsonFilePathDDB);

            deposit.RemoveAt(1);

            WriteJson(jsonFilePathDDB, deposit);
        }

        public void TransferFunds(List<Deposit> sender, List<Deposit> recipient)
        {
            int amount = rnd.Next(10000);

            sender.FirstOrDefault().AmountFunds -= amount;
            recipient.First().AmountFunds += amount;
        }
        #endregion

        #region конструкторы

        #endregion
    }
}
