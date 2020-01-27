using System;

namespace CustomerServiceApi.Models
{
    public class CustomerLedger
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int BatchId { get; set; }
        public int CustomerMasterId { get; set; }
        public CustomerMaster CustomerMaster { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}