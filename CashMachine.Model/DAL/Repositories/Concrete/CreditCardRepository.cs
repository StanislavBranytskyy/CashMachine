using CashMachine.Model.DAL.Repositories.Abstraction;
using System;
using CashMachine.Model.Entities;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CashMachine.Model.DTOs;

namespace CashMachine.Model.DAL.Repositories.Concrete
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private CashMachineContext _context;
        public CreditCardRepository(CashMachineContext context)
        {
            _context = context;
        }

        public Task<CreditCardValidityDTO> GetCreditCardValidity(string creditCardNumber)
        {
            return _context.CreditCards
                .Where(x => x.CreditCardNumber == creditCardNumber)
                .Select(x => new CreditCardValidityDTO
                {
                    CreditCardId = x.CreditCardId,
                    isValid = x.isValid
                })
                .FirstAsync();
        }

        public Task<CreditCard> GetCreditCard(int creditCardId)
        {
            return _context.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
        }

        public bool IsPinMatched(string pin, int creditCardId)
        {
            return _context.CreditCards.Any(x => x.CreditCardId == creditCardId && x.CreditCardPin == pin);
        }

        public async Task<int> PinFailedAttempt(int creditCardId)
        {
            var creditCard = await _context.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
            creditCard.FailedAttempts++;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return creditCard.FailedAttempts;
        }

        public async Task SetBalance(int creditCardId, decimal balance)
        {
            var creditCard = await _context.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
            creditCard.Balance = balance;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task BlockCreditCard(int creditCardId)
        {
            var creditCard = await _context.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
            creditCard.isValid = false;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
