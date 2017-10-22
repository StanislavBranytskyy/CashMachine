

using System.ComponentModel.DataAnnotations;

namespace CashMachine.Models
{
    public class CreditCardModel
    {
        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Input should contain 16 numbers")]
        public string CreditCardNumber { get; set; }
    }
}