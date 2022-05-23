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
            var path = Path.Combine(Environment.CurrentDirectory, "DepositDB.json");
            var sr = File.ReadAllText(path);
            List<Deposit> deposit = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Deposit>>(sr);
            return deposit;
        }

        public List<Deposit> OpenDeposit()
        {

            string[] DepositTypeArr = new string[2] { "Капитализированный", "Некапитализированный" };
            string filePath = jsonFilePathDDB;
                JsonSerializer jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                using StreamWriter sw = new(filePath, true);
                List<Deposit> deposit = new();
                    deposit.Add(new Deposit()
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
                        ClientId = rnd.Next(1, 3)
                    });
                jsonSerializer.Serialize(sw, deposit);
            return deposit;
        }

        //public List<Deposit> SaveChanges()
        //{
        //    JsonSerializer jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
        //    { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        //    using StreamWriter sw = new(jsonFilePathDDB, true);
        //    List<Deposit> deposits = new List<Deposit>();
        //    foreach (var deposit in deposits)
        //    {
        //        sw.WriteLine(new Deposit()
        //        {
        //            DepositNumber = deposit.DepositNumber,
        //            AmountFunds = deposit.AmountFunds,
        //            DepositType = deposit.DepositType,
        //            ClientId = deposit.ClientId,
        //        });
        //    }
        //    jsonSerializer.Serialize(sw, deposits);
        //    return deposits;
        //}
        #endregion

        #region конструкторы
        public DepositRepository()
        {
            Deposits = new List<Deposit>();
            Deposits = GetDeposits();
        }
        #endregion
    }
}
