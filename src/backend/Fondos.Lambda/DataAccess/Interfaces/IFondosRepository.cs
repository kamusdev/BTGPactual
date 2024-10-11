using Fondos.Lambda.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondos.Lambda.DataAccess.Interfaces
{
    public interface IFondosRepository
    {
        Task<IClient> GetClientByIdAsync(string id);
        Task<IEnumerable<IFund>> GetFundsAsync();
        Task PostOpeningAsync(IOpening mandate);
        Task PostClosureAsync(IClosure closure);
        Task<IEnumerable<IMandate>> GetMandatesAsync();
        Task<IEnumerable<ITransaction>> GetTransactionsAsync();
    }
}
