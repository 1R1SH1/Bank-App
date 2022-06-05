using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bank_A_WpfApp
{
    public class DepositRepository
    {
        private const string jsonFilePathDDB = "DepositDB.json";
        public Client selectedClient = new();

        public List<Deposit> GetAllDeposits()
        {

            List<Deposit> deposit = ReadJson(jsonFilePathDDB);
            return deposit;
        }

        public void SaveDeposits(List<Deposit> deposit)
        {
            WriteJson(jsonFilePathDDB, deposit);
        }

        private void WriteJson(string filePath, List<Deposit> deposit)
        {
            string json = JsonConvert.SerializeObject(deposit);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(json);
            }
        }

        private List<Deposit> ReadJson(string filePath)
        {
            if (!File.Exists(jsonFilePathDDB))
            {
                return new List<Deposit>();
            }
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadToEnd();

                return JsonConvert.DeserializeObject<List<Deposit>>(json);
            }
        }

        internal void RemoveDepositByDepositNumber(string depositNumber)
        {
            var deposits = GetAllDeposits();
            SaveDeposits(deposits.Where(d => d.DepositNumber != depositNumber).ToList());
        }
    }
}
