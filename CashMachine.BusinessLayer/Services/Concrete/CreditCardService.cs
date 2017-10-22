using CashMachine.BusinessLayer.Services.Abstraction;
using CashMachine.BusinessLayer.Utils;
using CashMachine.Model.Constans;
using CashMachine.Model.DAL.Repositories.Abstraction;
using CashMachine.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashMachine.BusinessLayer.Services.Concrete
{
    public class CreditCardService : ICreditCardService
    {
        private ICreditCardRepository _creditCardRepository;
        public CreditCardService(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        public async Task<CreditCardValidityDTO> IsCreditCardValid(string creditCardNumber)
        {
            var encryptedCreditNumber = Cipher.Encrypt(creditCardNumber, CipherConstans.CreditCardKey);
            var  isValid = new CreditCardValidityDTO();
            try
            {
                isValid = await _creditCardRepository.GetCreditCardValidity(encryptedCreditNumber);
            }
            catch (Exception ex)
            {
                throw new Exception("Credit card number wasn't found");
            }
            
            return isValid;
        }

        public bool IsPinValid(string pin, int creditCardId)
        {
            var encryptedPin = Cipher.Encrypt(pin, CipherConstans.CreditCardPin);
            var isMatched = _creditCardRepository.IsPinMatched(encryptedPin, creditCardId);
            return isMatched;
        }

        public async Task PinFailedAttempt(int creditCardid)
        {
            var creditAttemptsCount = await _creditCardRepository.PinFailedAttempt(creditCardid);
            if (creditAttemptsCount == 4)
            {
                await _creditCardRepository.BlockCreditCard(creditCardid);
                throw new Exception("Credit Card is blocked!");
            }
        }

        public async Task<CreditCardDTO> GetCreditCard(int creditCardId)
        {
            var creditCard = await _creditCardRepository.GetCreditCard(creditCardId);
            if (creditCard == null)
            {
                throw new Exception("Credit card number wasn't found");
            }
            var creditCardNumber = Cipher.Decrypt(creditCard.CreditCardNumber, CipherConstans.CreditCardKey);

            return new CreditCardDTO
            {
                CreditCardId = creditCard.CreditCardId,
                CreditCardNumber = creditCardNumber,
                isValid = creditCard.isValid,
                Pin = creditCard.CreditCardPin,
                Balance = creditCard.Balance
            };
        }
    }
}
