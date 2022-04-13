using Microsoft.AspNetCore.Mvc;
using project1.Models;
using System;
using System.Collections.Generic;

namespace project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        Customer customerModel = new Customer();

        [HttpGet]
        [Route("GetCustomers")]
        public IActionResult GetAllCustomers()
        {
            return Ok(customerModel.GetAllCustomers());
        }

        [HttpGet]
        [Route("GetCustomerById")]
        public IActionResult GetCustomerById(int id)
        {
            Customer cu = customerModel.GetCustomerById(id);

            if (cu == null)
            {
                return BadRequest("Invalid customer ID");
            }
            return Ok(cu);
        }

        [HttpPost]
        [Route("AddCustomer")]
        // as far as i can tell, any object passed into the function will be turned into a json object in swagger, which allows you to type all the info
        public IActionResult AddCustomer(string firstName, string lastName, string street, string city, string state, int zip)
        {
            try
            {
                return Created("", customerModel.AddCustomer(firstName, lastName, street, city, state, zip));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
