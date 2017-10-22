using System;
using System.ComponentModel.DataAnnotations;

namespace CashMachine.Models
{
    public class BalanceModel
    {
        public int CreditCardId { get; set; }
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }
        [Display(Name = nameof(Date))]
        public DateTime Date { get; set; }
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
    }
}