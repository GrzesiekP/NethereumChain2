using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using NethereumChain2.Core.Models;
using Nethereum.Contracts;
using Nethereum.Web3;
using NethereumChain2.Core.Logging;
using NethereumChain2.Core.Configuration;

namespace NethereumChain2.Core.Contracts
{
    public class SupplyContractRepository : ISupplyContractRepository
    {
        private readonly Contract _contract;
        private readonly Web3 _web3;
        private readonly INethereumLogger _nethereumLogger;

        public SupplyContractRepository(INethereumLogger nethereumLogger)
        {
            _nethereumLogger = nethereumLogger ?? throw new ArgumentNullException(nameof(nethereumLogger));

            var address = AppSettingsProvider.ContractAddress ?? throw new ArgumentNullException(nameof(AppSettingsProvider.ContractAddress));
            _web3 = new Web3(AppSettingsProvider.InfuraApiAddress);
            _contract = new BaseContract(address, _web3).Contract;
        }

        public SupplyContractRepository(string address, Web3 web3, INethereumLogger nethereumLogger)
        {
            _web3 = web3 ?? throw new ArgumentNullException(nameof(web3));
            _contract = new BaseContract(address, web3).Contract;

            _nethereumLogger = nethereumLogger ?? throw new ArgumentNullException(nameof(nethereumLogger));
        }

        public async Task<string> AddNewLocationAsync(TransactionDetails transactionDetails, CreateLocationCommand location)
        {
            try
            {
                var addLocationFunction = _contract.GetFunction("AddNewLocation");
                var data = addLocationFunction.GetData(location.LocationName);

                var txCount = await _web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(transactionDetails.UserAddress).ConfigureAwait(false);

                var encoded = Web3.OfflineTransactionSigner.SignTransaction(
                    transactionDetails.PrivateKey, 
                    _contract.Address, 
                    transactionDetails.Amount, 
                    txCount.Value, 
                    1000000000000L, 
                    transactionDetails.GasLimit, 
                    data
                );

                var transaction = await _web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(encoded).ConfigureAwait(false);

                return transaction;
            }
            catch (Exception e)
            {
                _nethereumLogger.Error(e.Message);
                return null;
            }
        }

        public async Task<ImmutableList<Response>> GetAllLocations()
        {
            var locations = ImmutableList.Create<Response>();
            var getLocationFunction = _contract.GetFunction("GetLocation");

            for (var i = 0; i <= GetChainCount().Result; i++)
            {
                try
                {
                    var locationName = await _contract.GetFunction("ChainDictionary")
                        .CallAsync<string>(i);

                    var location = await getLocationFunction
                        .CallDeserializingToObjectAsync<Response>(locationName)
                        .ConfigureAwait(false);
                    locations = locations.Add(location);
                    // map repsonses to locations
                }
                catch (Exception e)
                {
                    _nethereumLogger.Error(e.Message);
                    return null;
                }
            }

            return locations;
        }

        public async Task<int> GetChainCount() 
            => await _contract.GetFunction("GetChainCount").CallAsync<int>();

        public async Task<Response> GetLocation(string locationName)
        {
            if (string.IsNullOrEmpty(locationName))
            {
                _nethereumLogger.Error("Location name is empty.");
                return null;
            }

            try
            {
                var getLocationFunction = _contract.GetFunction("GetLocation");
                var location = await getLocationFunction
                    .CallDeserializingToObjectAsync<Response>(locationName)
                    .ConfigureAwait(false);

                return location;
            }
            catch (Exception e)
            {
                _nethereumLogger.Error(e.Message);
                return null;
            }
        }

        public async Task<string> SignApproval(TransactionDetails transactionDetails)
        {
            try
            {
                var addLocationFunction = _contract.GetFunction("SignApproval");

                var txCount = await _web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(transactionDetails.UserAddress).ConfigureAwait(false);

                var encoded = Web3.OfflineTransactionSigner.SignTransaction(
                    transactionDetails.PrivateKey,
                    _contract.Address,
                    transactionDetails.Amount,
                    txCount.Value,
                    1000000000000L,
                    transactionDetails.GasLimit
                );

                var transaction = await _web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(encoded).ConfigureAwait(false);

                return transaction;
            }
            catch (Exception e)
            {
                _nethereumLogger.Error(e.Message);
                return null;
            }
        }
    }
}
