
namespace CashMachine.Model.DTOs
{
    public class CreditCardDTO : CreditCardValidityDTO
    {
        public string CreditCardNumber { get; set; }
        public string Pin { get; set; }
        public decimal Balance { get; set; }
    }
}
