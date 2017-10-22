
using System;
using System.ComponentModel.DataAnnotations;

namespace CashMachine.Models
{
    public class ReportModel
    {
        public int CreditCardId { get; set; }
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }
        [Display(Name = nameof(Balance))]
        public decimal Balance { get; set; }
        [Display(Name = "Date")]
        public DateTime DateOfWithdrawal { get; set; }
        [Display(Name = "Withdrawal amount")]
        public decimal WithdrawalAmount { get; set; }
    }
}