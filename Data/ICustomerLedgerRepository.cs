using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerServiceApi.Models;

namespace CustomerServiceApi.Data
{
    public interface ICustomerLedgerRepository
    {         
        Task<CustomerLedger> CreateCustomerLedger(CustomerLedger customerLedger);
        CustomerLedger UpdateCustomerLedger(CustomerLedger customerLedger);
        void DeleteCustomerLedger(int customerLedgerId);
        Task<IEnumerable<CustomerLedger>> GetAllCustomerLedger();
        Task<CustomerLedger> GetCustomerLedgerById(int CustomerLedgerId);
    }
}