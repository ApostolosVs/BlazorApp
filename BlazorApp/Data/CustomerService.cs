using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BlazorApp.Data
{
    public class CustomerService : ICustomerService  //The customer service class implented the ICustomerService interface!!
    {
        private readonly IMongoCollection<Customer> _cust; //The variable for the Mongodatabase
        public CustomerService(IMongoDbSettings settings)//The constructor of CustomerService class and define the parameters for the database!
        {
            var client = new MongoClient(settings.ConnectionString); 
            var database = client.GetDatabase(settings.DatabaseName); 

            _cust = database.GetCollection<Customer>(settings.CollectionName); 
        }
        //The function that create the customer in the database
        public Customer CreateCustomer(Customer cust)
        {
            try
            {
                 _cust.InsertOneAsync(cust); //We insert a customer in the database with our help of _cust variable!!
                return cust;
            }catch{
                throw;
            }
        }

        //The function tha delete a customer with a specific id!!
        public  void DeleteCustomer(string id)
        {
            try
            {
                 _cust.DeleteOneAsync(cust => cust.Id == id); //Delete the customer which the customer id (from the database) is equal to the id of function parameter!
                
            }
            catch
            {
                throw;
            }
        }
        //Function that edit a specific customer and replace the new version of customer with the old.
        public  void EditCustomer( Customer cust)
        {
            try
            {
                 _cust.ReplaceOneAsync(customer => customer.Id == cust.Id, cust); //The change become based on customer id!!
                
            }
            catch
            {
                throw;
            }
        }
        //The function which return the list with all customers that included in the database!
        public List<Customer> GetCustomers()
        {
            try
            {
                return _cust.Find(Customer => true).ToList(); //For all the customers that find in the database create a list (ToList())
            }
            catch
            {
                throw;
            }
        }





        //The function which return the list with all customers that included in the database!
        public List<Customer> GetCustomers(int pageid,int pagesize)
        {
            int entries = pageid * pagesize;
            try
            {
                return  _cust.Find(Customer => true).Skip(entries).Limit(pagesize).ToList(); //For all the customers that find in the database create a list (ToList())
            }
            catch
            {
                throw;
            }
        }
        //The function which return the data of a specific customer with id !!
        public Customer SingleCustomer(string id)
        {
            try
            {
                return _cust.Find<Customer>(customer => customer.Id == id).FirstOrDefault(); //With the _cust variable find the customer with the same id from the input and return their data!
            }
            catch
            {
                return null;
            }
        }
    }
}
