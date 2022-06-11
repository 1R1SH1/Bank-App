using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_A_WpfApp.Classes
{
    public static class Extensions
    {
        public static void AddFunds(this Deposit recipient, int amount)
        {
            recipient.AmountFunds += amount;
        }

        public static void DeductFunds(this Deposit deposit, int amount)
        {
            deposit.AmountFunds -= amount;
        }
    }
}
