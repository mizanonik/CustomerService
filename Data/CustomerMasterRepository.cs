using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceApi.Data
{
    public class CustomerMasterRepository : ICustomerMasterRepository
    {
        private readonly CustomerServiceContext _context;
        public CustomerMasterRepository(CustomerServiceContext context)
        {
            _context = context;

        }
        public async Task<CustomerMaster> CreateCustomer(CustomerMaster customerMaster)
        {
            try{
                await _context.CustomerMasters.AddAsync(customerMaster);
                await _context.SaveChangesAsync();

                return customerMaster;
            }
            catch(Exception){
            }
            return null;
        }

        public void DeleteCustomer(int CustomerId)
        {
            var customer = _context.CustomerMasters
                                .FirstOrDefault(c => c.Id == CustomerId);
            if(customer != null)
                _context.CustomerMasters.Remove(customer);
                _context.SaveChanges();
        }

        public CustomerMaster EditCustomer(CustomerMaster customerMaster)
        {
            _context.CustomerMasters.Update(customerMaster);
            _context.SaveChanges();

            return _context.CustomerMasters.FirstOrDefault(c=>c.Id == customerMaster.Id);
        }

        public async Task<CustomerMaster> GetCustomer(int CustomerId)
        {
            var customer = await _context.CustomerMasters
                                .FirstOrDefaultAsync(c=>c.Id == CustomerId);
            if(customer == null)
                return null;
            
            return customer;
        }

        public async Task<List<CustomerMaster>> GetCustomers()
        {
            var customers = await _context.CustomerMasters.ToListAsync();
            return customers;
        }
    }
}