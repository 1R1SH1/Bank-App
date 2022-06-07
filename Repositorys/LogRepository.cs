using Bank_A_WpfApp.Classes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bank_A_WpfApp.Repositorys
{
    internal class LogRepository
    {

        private const string jsonFilePathLDB = "LogDB.json";

        public List<InfoLog> GetAllInfoLog()
        {

            List<InfoLog> log = ReadJson(jsonFilePathLDB);
            return log;
        }

        public void SaveInfoLog(List<InfoLog> log)
        {
            WriteJson(jsonFilePathLDB, log);
        }

        private void WriteJson(string filePath, List<InfoLog> log)
        {
            string json = JsonConvert.SerializeObject(log);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(json);
            }
        }

        private List<InfoLog> ReadJson(string filePath)
        {
            if (!File.Exists(jsonFilePathLDB))
            {
                return new List<InfoLog>();
            }
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadToEnd();

                return JsonConvert.DeserializeObject<List<InfoLog>>(json);
            }
        }
    }
}
