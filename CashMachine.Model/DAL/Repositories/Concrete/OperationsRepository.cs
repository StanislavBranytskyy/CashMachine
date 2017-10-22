using CashMachine.Model.DAL.Repositories.Abstraction;
using CashMachine.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine.Model.DAL.Repositories.Concrete
{
    public class OperationsRepository : IOperationsRepository
    {
        private readonly CashMachineContext _context;
        public OperationsRepository(CashMachineContext context)
        {
            _context = context;
        }

        public async Task AddOperation(int creditCardId, int code, string text)
        {
            var operation = new Operations
            {
                CreatedDate = DateTime.Now,
                CreditCardId = creditCardId,
                OperationCode = code, 
                OperationText = text
            };

            _context.Operations.Add(operation);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
