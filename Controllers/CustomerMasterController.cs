using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace CustomerServiceApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerMasterController : ControllerBase
    {
         private readonly ICustomerMasterRepository _repository;
        public CustomerMasterController(ICustomerMasterRepository customerMasterRepository)
        {
            _repository = customerMasterRepository;
        }

        [HttpPost]
        public async Task<IActionResult> createCustomer([FromBody] CustomerMaster customerMaster){
            
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var savedCustomer = await _repository.CreateCustomer(customerMaster);
            if(savedCustomer==null){
                return BadRequest("Failed to save the customer");
            }
            return Created("Successfully saved the customer",customerMaster);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCustomer(){
            List<CustomerMaster> customers = await _repository.GetCustomers();
            if(customers == null){
                return BadRequest();
            }
            return Ok(customers);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerById(int customerId){
            var customer = await _repository.GetCustomer(customerId);
            if(customer == null){
                return BadRequest();
            }
            return Ok(customer);
        }
        [HttpPost]
        public IActionResult UpdateCustomer([FromBody] CustomerMaster customerMaster){
            
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var customer = _repository.EditCustomer(customerMaster);
            if(customer == null){
                return BadRequest("Failed to save the customer");
            }
            return Created("Successfully Updated the customer",customer);
        }
        [HttpDelete]
        public IActionResult DeleteCustomer(int customerId){
            
             _repository.DeleteCustomer(customerId);

            return Ok("Bank Deleted ");
        }
        [HttpGet]
        public async Task<IActionResult> SendCustomerDataToBankService(int customerId){
            try{
                var factory = new ConnectionFactory(){HostName="rabbitmq"};
                using(var connection = factory.CreateConnection()){
                    using(var channel = connection.CreateModel()){
                        channel.QueueDeclare(
                            queue: "customer_queue",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                        );
                        //var msg = await GetCustomerById(customerId);
                        var msg = await GetCustomerById(customerId);
                        // var options = new JsonSerializerOptions{
                        //     WriteIndented = true
                        // };
                        var message = JsonConvert.SerializeObject(msg);
                        //var message = "Hello from customer";
                        var body = Encoding.UTF8.GetBytes(message);

                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: "customer_queue",
                            basicProperties: properties,
                            body: body
                        );
                        return Ok("Send customer data");
                    }
                }
            }
            catch(Exception){}
            return BadRequest("failed to send the customer data");
        }
        // public async Task<IActionResult> SendCustomerDataToBankService(int customerId){
        //     try{
        //         var factory = new ConnectionFactory(){HostName="localhost"};
        //         using(var connection = factory.CreateConnection()){
        //             using(var channel = connection.CreateModel()){
        //                 channel.ExchangeDeclare(
        //                     exchange: "customerInfo",
        //                     type: ExchangeType.Fanout
        //                 );
        //                 var msg = await GetCustomerById(customerId);
        //                 // var options = new JsonSerializerOptions{
        //                 //     WriteIndented = true
        //                 // };
        //                 var message = JsonConvert.SerializeObject(msg);
        //                 var body = Encoding.UTF8.GetBytes(message);
        //                 channel.BasicPublish(
        //                     exchange: "customerInfo",
        //                     routingKey: "",
        //                     basicProperties: null,
        //                     body: body
        //                 );
        //                 return Ok("Send customer data");
        //             }
        //         }
        //     }
        //     catch(Exception){}
        //     return BadRequest("failed to send the customer data");
        // }
    }
}