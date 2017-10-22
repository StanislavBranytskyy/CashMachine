using System.ComponentModel.DataAnnotations;

namespace CashMachine.Models
{
    public class PinModel
    {
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Pin should contain 4 numbers")]
        public string Pin { get; set; }
        public int CreditCardId { get; set; }
    }
}