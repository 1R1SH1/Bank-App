using System;

namespace Bank_A_WpfApp.Classes
{
    class NullReferenceException : ApplicationException
    {
        public NullReferenceException(string message) : base(message) { }
    }
}
