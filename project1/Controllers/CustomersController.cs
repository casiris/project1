using Microsoft.AspNetCore.Mvc;
using project1.Models;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        Customer model = new Customer();

        [HttpGet]
        [Route("GetCustomers")]
        public IActionResult GetAllCustomers()
        {
            // will need to try/catch this eventually
            return Ok(model.GetAllCustomers());
        }

        [HttpGet]
        [Route("GetCustomerById")]
        public IActionResult GetCustomerById(int id)
        {
            return Ok(model.GetCustomerById(id));
        }

        [HttpPost]
        [Route("AddCustomer")]
        // as far as i can tell, any object passed into the function will be turned into a json object in swagger, which allows you to type all the info
        public IActionResult AddCustomer(Customer customer)
        {
            return Created("", model.AddCustomer(customer));
        }
    }
}
