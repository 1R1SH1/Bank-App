﻿using System.Collections.Generic;

namespace Bank_A_WpfApp
{
    public class Client
    {
        public int Id { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public List<Deposit> Deposits { get; set; } = new();

    }
}
