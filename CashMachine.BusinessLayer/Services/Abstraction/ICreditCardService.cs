using CashMachine.Model.DTOs;
using System.Threading.Tasks;

namespace CashMachine.BusinessLayer.Services.Abstraction
{
    public interface ICreditCardService
    {
        Task<CreditCardValidityDTO> IsCreditCardValid(string creditCardNumber);
        bool IsPinValid(string pin, int creditCardId);
        Task<CreditCardDTO> GetCreditCard(int creditCardId);
        Task PinFailedAttempt(int creditCardid);
    }
}
