using System;
using System.ComponentModel.DataAnnotations;

namespace CashMachine.Model.Entities
{
    public class Operations
    {
        [Key]
        public int OperationId { get; set; }
        public string OperationText { get; set; }
        public int OperationCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreditCardId { get; set; }
    }
}
