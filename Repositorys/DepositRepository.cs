﻿using Newtonsoft.Json;
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
        public List<Deposit> Deposits { get; set; }
        #endregion

        #region методы

        private const string jsonFilePathDDB = "DepositDB.json";

        Random rnd = new();

        /// <summary>
        /// заполнение базы случаными счетами
        /// </summary>
        /// <returns></returns>
        public List<Deposit> InitialDDB()
        {
            string[] DepositTypeArr = new string[2] { "Капитализированный", "Некапитализированный" };
            List<Deposit> deposit = new();
            for (int i = 0; i < 4; i++)
                deposit.Add(new Deposit
                {
                    DepositNumber = $"{rnd.Next(10)}" +
                                    $"{rnd.Next(10)}{rnd.Next(10)}" +
                                    $"{rnd.Next(10)} {rnd.Next(10)}" +
                                    $"{rnd.Next(10)}{rnd.Next(10)}" +
                                    $"{rnd.Next(10)}{rnd.Next(10)}" +
                                    $"{rnd.Next(10)}{rnd.Next(10)}" +
                                    $"{rnd.Next(10)}{rnd.Next(10)}" +
                                    $"{rnd.Next(10)}{rnd.Next(10)}" +
                                    $"{rnd.Next(10)}",
                    AmountFunds = $"{rnd.Next(10)}{rnd.Next(10)}" +
                                    $"{rnd.Next(10)}{rnd.Next(10)}" +
                                    $"{rnd.Next(10)}{rnd.Next(10)}",
                    DepositType = DepositTypeArr[rnd.Next(2)],
                    ClientId = i % 2 + 1
                });
            return deposit;

            //var userFullName = Console.ReadLine();

            //string[] props = userFullName.Split(',');

            //string DepositNumber = props[0];
            //string AmountFunds = props[1];
            //string DepositType = props[3];
            //int ClientId = int.Parse(props[4]);

            //var deposit = new Deposit(DepositNumber,
            //                          AmountFunds,
            //                          DepositType,
            //                          ClientId);

            //AddDeposit(deposit);
            //SaveChanges();
        }

        public List<Deposit> GetDeposits()
        {
            using StreamReader sr = File.OpenText("DepositDB.json");
            List<Deposit> deposit = new();
            deposit = InitialDDB();
            return deposit;
        }

        public List<Deposit> AddDeposit(Deposit deposit)
        {
            Deposits.Add(deposit);
            return Deposits;
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

        public void SaveChanges()
        {
            using StreamWriter sw = new("DepositDB.json", true);
            List<Deposit> deposits = new();
            foreach (var deposit in deposits)
            {
                sw.WriteLine(deposit.DepositNumber +
                             deposit.AmountFunds +
                             deposit.DepositType +
                             deposit.ClientId);
            }
            //using StreamWriter sr = File.CreateText(@"C:\\Users\\Rishat Murzyev\\source\\repos\\Bank_A_WpfApp\\bin\\Debug\\DepositDB.json");
            //List<Deposit> deposit = new();
            //deposit = InitialDDB();
            //return deposit;
        }
        #endregion

        #region конструкторы
        /// <summary>
        /// Репозиторий
        /// </summary>
        /// <param name="clientType">тип отображения консультант или менеджер</param>
        public DepositRepository()
        {
            Deposits = new List<Deposit>();
            LoadDepositData();
            SaveDepositData(InitialDDB());
        }
        #endregion
    }
}