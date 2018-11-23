using Nethereum.Contracts;
using Nethereum.Web3;
using NethereumChain2.Core.Configuration;
using NethereumChain2.Core.Utilities;

namespace NethereumChain2.Core.Contracts
{
    public class BaseContract
    {
        public Contract Contract { get; }

        public BaseContract(string address, Web3 web3)
        {
            var abi = ContractUtilities.GetAbiFromFile(AppSettingsProvider.AbiFilePath); //TODO: abi file w configu @"solc\ABI\SupplyChainAbi.txt"
            Contract = web3.Eth.GetContract(abi, address);
        }
    }
}