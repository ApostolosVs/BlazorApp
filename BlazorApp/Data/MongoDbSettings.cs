using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class MongoDbSettings : IMongoDbSettings //The  class MongoDbSettings implented the IMongoDbSettings interface!!
    {
        //Because these informations are sensitive I use Encapsulation in order to be hidden.
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
