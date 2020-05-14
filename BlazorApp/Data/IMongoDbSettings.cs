using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    /*The interface for the settings database!!The class should contain 3 parameters 
     * the collection name (our case:Customers)
     * the connectionString 
     * and the database name(our case CustomerDB)
    */

    public interface IMongoDbSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
