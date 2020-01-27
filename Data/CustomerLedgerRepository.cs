using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceApi.Data
{
    public class CustomerLedgerRepository : ICustomerLedgerRepository
    {
        private readonly CustomerServiceContext _context;
        public CustomerLedgerRepository(CustomerServiceContext context)
        {
            _context = context;

        }
        public async Task<CustomerLedger> CreateCustomerLedger(CustomerLedger customerLedger)
        {
            try{
                await _context.CustomerLedgers.AddAsync(customerLedger);
                await _context.SaveChangesAsync();

                return customerLedger;
            }
            catch(Exception){}

            return null;
        }

        public void DeleteCustomerLedger(int customerLedgerId)
        {
            var customer = _context.CustomerLedgers.FirstOrDefault(cl=>cl.Id == customerLedgerId);
            _context.CustomerLedgers.Remove(customer);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<CustomerLedger>> GetAllCustomerLedger()
        {
            var customerLedger = await _context.CustomerLedgers.ToListAsync();
            return customerLedger;
        }

        public async Task<CustomerLedger> GetCustomerLedgerById(int CustomerLedgerId)
        {
            var customer = await _context.CustomerLedgers.FirstOrDefaultAsync(c => c.Id == CustomerLedgerId);
            if(customer == null)
                return null;
            return customer;
        }

        public CustomerLedger UpdateCustomerLedger(CustomerLedger customerLedger)
        {
            _context.CustomerLedgers.Update(customerLedger);
            _context.SaveChanges();

            return _context.CustomerLedgers.FirstOrDefault(c=>c.Id == customerLedger.Id);
        }
    }
}