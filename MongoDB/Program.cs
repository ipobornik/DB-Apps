
namespace MongoDB
{

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
    
    class Program
    {
        static void Main(string[] args)
        {
            
            // INFO : 
            // http://docs.mongodb.org/ecosystem/tutorial/getting-started-with-csharp-driver/

            // Get a Reference to the Client Object
            var connectionString = "mongodb://localhost/dbApps-Core1.0/data/mysql/"; // MySQL
            // var connectionString = "mongodb://localhost/dbApps-Core1.0/data/mssql/"; // MSSQL
            var client = new MongoClient(connectionString);

            //Get a Reference to a Server Object
            var server = client.GetServer();

            //Get a Reference to a Database Object
            var dataBaseName = "supermarkets_chain";
            var database = server.GetDatabase(dataBaseName);

            // Get a Reference to a Collection Object
            // You would get a reference to a collection containing Entity documents like this:
            var entitiesName = "product";
            var collection = database.GetCollection<Entity>(entitiesName);

            //isnert entity
            var entity = new Entity
            {
                Name = "Tom"
            };
            collection.Insert(entity);
            var id = entity.Id; // In


            // get entity
            var query = Query<Entity>.EQ(e => e.Id, id);
            var entityTo = collection.FindOne(query);
            Console.WriteLine(entityTo.Name);

        }

        public class Entity
        {
            public ObjectId Id { get; set; }

            public string Name { get; set; }

            public string Vendor { get; set; }

            public int QuantitySold { get; set; }

            public decimal TotalIncome { get; set; }
        }
    }
}
