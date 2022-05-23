namespace Bank_A_WpfApp
{
    public class Deposit
    {
        #region поля

        #endregion

        #region свойства
        [Newtonsoft.Json.JsonProperty("DepositNumber")]
        public string DepositNumber { get; set; }
        [Newtonsoft.Json.JsonProperty("AmountFunds")]
        public int AmountFunds { get; set; }
        [Newtonsoft.Json.JsonProperty("DepositType")]
        public string DepositType { get; set; }
        [Newtonsoft.Json.JsonProperty("ClientId")]
        public int ClientId { get; set; }
        #endregion

        #region методы

        #endregion

        #region конструкторы

        #endregion
    }
}
