using System.Collections.Generic;

namespace Bank_A_WpfApp.Classes
{
    public class InfoLog
    {
        public List<string> log = new List<string>();

        public void AddToLog(string msg) => log.Add(msg);
    }
}
