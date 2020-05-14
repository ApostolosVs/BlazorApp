using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//The interface which describe the functionalities of the customer service!!
namespace BlazorApp.Data
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers(int pageid, int pagesize); //The function that return the list with customers!!Depends of how many entries displayed and the page id!!
        Customer CreateCustomer(Customer cust); //The function which create a new customer!
        void EditCustomer(Customer cust); //The function that edit an existing customer
       Customer SingleCustomer(string id); //The function that return the data of a specific customer!!
        void DeleteCustomer(string id); //The function that delete a customer with a specific id!!
    }
}
