using System.Threading.Tasks;

namespace CashMachine.Model.DAL.Repositories.Abstraction
{
    public interface IOperationsRepository
    {
        Task AddOperation(int creditCardId, int code, string text);
    }
}
