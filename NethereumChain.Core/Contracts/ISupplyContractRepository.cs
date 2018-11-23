using NethereumChain2.Core.Models;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace NethereumChain2.Core.Contracts
{
    public interface ISupplyContractRepository
    {
        Task<int> GetChainCount();
        Task<Response> GetLocation(string locationName);
        Task<ImmutableList<Response>> GetAllLocations();
        Task<string> AddNewLocationAsync(TransactionDetails transactionDetails, CreateLocationCommand location);
        Task<string> SignApproval(TransactionDetails transactionDetails);
    }
}