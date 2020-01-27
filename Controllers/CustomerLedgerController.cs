using System.Threading.Tasks;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CustomerServiceApi.Data;

namespace CustomerServiceApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerLedgerController : ControllerBase
    {
        private readonly ICustomerLedgerRepository _repository;
        public CustomerLedgerController(ICustomerLedgerRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomerLedger([FromBody] CustomerLedger customerLedger){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var customer = await _repository.CreateCustomerLedger(customerLedger);
            if(customer == null){
                return BadRequest("Failed to save the customer ledger");
            }
            return Ok(customerLedger);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomerLedger(){
            var customers = await _repository.GetAllCustomerLedger();
            if(customers == null)
                return BadRequest();
            return Ok(customers);
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerLedgerById(int customerLedgerId){
            var customer = await _repository.GetCustomerLedgerById(customerLedgerId);
            if(customer == null)
                return BadRequest();
            return Ok(customer);
        }
        [HttpPost]
        public IActionResult UpdateCustomer([FromBody] CustomerLedger customerLedger){
            
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var customer = _repository.UpdateCustomerLedger(customerLedger);
            if(customer == null){
                return BadRequest("Failed to save the customer");
            }
            return Created("Successfully Updated the customer",customer);
        }
        [HttpDelete]
        public IActionResult DeleteCustomer(int customerLedgerId){
            
             _repository.DeleteCustomerLedger(customerLedgerId);

            return Ok("Customer Deleted ");
        }
    }
}