using CashMachine.Model.DTOs;
using CashMachine.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashMachine.Model.DAL.Repositories.Abstraction
{
    public interface ICreditCardRepository
    {
        Task<CreditCardValidityDTO> GetCreditCardValidity(string creditCardNumber);
        Task<CreditCard> GetCreditCard(int creditCardId);
        bool IsPinMatched(string pin, int creditCardId);
        Task<int> PinFailedAttempt(int creditCardId);
        Task BlockCreditCard(int creditCardId);
        Task SetBalance(int creditCardId, decimal balance);
    }
}
