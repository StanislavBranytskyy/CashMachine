
using System.ComponentModel.DataAnnotations;

namespace CashMachine.Model.Entities
{
    public class CreditCard
    {
        [Key]
        public int CreditCardId { get; set; }
        public bool isValid { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardPin { get; set; }
        public decimal Balance { get; set; }
        public int FailedAttempts { get; set; }
    }
}
