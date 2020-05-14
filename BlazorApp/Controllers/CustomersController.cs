using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Data;
using BlazorApp.Models;

//This class used for the api calls!!
namespace BlazorApp.Controllers 
{
    //The Route define the url that used for the apis!!In our case is the url/api/...
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase 
    {
        private readonly CustomerService _context;

        public CustomersController(CustomerService context)
        {
            _context = context;
        }

        //The below function is for the Get request in our case is the Http://localhost:<port>/api/Customers and return the data of all customers!
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
     

            return _context.GetCustomers(); //When we send a get request we call the function GetCustomer from the CustomerServices class.
        }


        //Now if we send get request with id parameter we have the below function:
        //Example is http://localhost<port>/api/Customers/1      (1 is the customer id)
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(string id) 
        {
            var customer = _context.SingleCustomer(id); //I use the function SingleCustomer from the CustomerService and find the data for the specific customer!!
            //If this id not contained in the database that mean that we dont have customer
            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }


        //If we send a post request the below function is executed:
        //With the post request we can create a new customer
        [HttpPost]
        public void Create(Customer cust)
        {
            _context.CreateCustomer(cust); //We call the function Create Customer from the customerservice!!

            
        }


        //The below function is for the put request and receive an id as parameter:
        //We used it in order to update-edit a specific customer!
        [HttpPut("{id}")]
        public void  Update(Customer customer)
        {
          

            _context.EditCustomer(customer);//We call again the function from the customerservice!!

  
        }


        //The function for the delete request:
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var cust = _context.SingleCustomer(id);  //I try to find the customer with the help of function Sigle Customer 

            if (cust == null)
            {
                return NotFound();
            }

            _context.DeleteCustomer(cust.Id); //Delete the customer with id with the fuction DeleteCustomer from the customerService

            return NoContent();
        }
    }
}
