namespace NethereumChain2.Core.Models
{
    public class TransactionDetails
    {
        public string UserAddress { get; set; }
        public string PrivateKey { get; set; }
        public int GasLimit { get; set; }
        public int Amount { get; set; }
    }
}
