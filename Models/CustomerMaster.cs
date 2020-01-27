using System;

namespace CustomerServiceApi.Models
{
    public class CustomerMaster
    {
        public int Id { get; set; } 
        public int CustomerId { get; set; } 
        public double Amount { get; set; }
        public DateTime LastTransactionDate { get; set; }
    }
}