using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bank_A_WpfApp
{
    public class DepositRepository
    {
        #region поля

        #endregion

        #region свойства
        Random rnd = new Random();
        public List<Deposit> Deposits { get; set; }
        #endregion

        #region методы

        public string jsonFilePathDDB = "DepositDB.json";

        public List<Deposit> GetDeposits()
        {
            List<Deposit> deposits = ReadJson(jsonFilePathDDB);
            return deposits;
        }

        public void WriteJson(string filePath, List<Deposit> trackDataList)
        {
            string json = JsonConvert.SerializeObject(trackDataList);

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

        public List<Deposit> OpenDeposit()
        {
            List<Deposit> myTrackDataList = ReadJson(jsonFilePathDDB);

            string[] DepositTypeArr = new string[2] { "Капитализированный", "Некапитализированный" };

            Deposit newTrackData = new Deposit();
            newTrackData.DepositNumber = $"{rnd.Next(10)}{rnd.Next(10)}" +
                                         $"{rnd.Next(10)}{rnd.Next(10)}" +
                                         $"{rnd.Next(10)}{rnd.Next(10)}" +
                                         $"{rnd.Next(10)}{rnd.Next(10)}" +
                                         $"{rnd.Next(10)}{rnd.Next(10)}" +
                                         $"{rnd.Next(10)}{rnd.Next(10)}" +
                                         $"{rnd.Next(10)}{rnd.Next(10)}" +
                                         $"{rnd.Next(10)}{rnd.Next(10)}";
            newTrackData.AmountFunds = rnd.Next(1000, 100000);
            newTrackData.DepositType = DepositTypeArr[rnd.Next(2)];
            newTrackData.ClientId = rnd.Next(1, 3);

            myTrackDataList.Add(newTrackData);

            WriteJson(jsonFilePathDDB, myTrackDataList);
            return myTrackDataList;
        }
        #endregion

        #region конструкторы
        public DepositRepository()
        {
            Deposits = new List<Deposit>();
            Deposits = OpenDeposit();
        }
        #endregion
    }
}
