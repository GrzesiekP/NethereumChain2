using Nethereum.Web3;
using NethereumChain2.Core.Contracts;
using NethereumChain2.Core.Logging;

namespace NethereumChain2.Core
{
    public class SupplyBlockchain
    {
        private readonly string _url;
        private Web3 Web3Api => new Web3(_url);

        public SupplyContractRepository SupplyChainContract { get; }

        public SupplyBlockchain(string url, string address, INethereumLogger nethereumLogger)
        {
            _url = url;
            SupplyChainContract = new SupplyContractRepository(address, Web3Api, nethereumLogger);
        }
    }
}