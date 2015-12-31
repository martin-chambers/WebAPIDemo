using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace WebApiDemo.Models
{
    public class ValuesRepository : IValuesRepository
    {
        string server;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<Value> values;
        
        public ValuesRepository(string connection)
        {
            client = new MongoClient(connection);
            database = client.GetDatabase("Values");
            values = database.GetCollection<Value>("Values");
        }
        public async void AddValueAsync(Value item)
        {
            item.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            await values.InsertOneAsync(item);                              
        }

        public async Task<IEnumerable<Value>> GetAllValuesAsync()
        {
            List<Value> valueList = await values.Find(new BsonDocument()).ToListAsync();
            return valueList;            
        }

        // not sure if the filter definition is set up correctly!!
        // http://mongodb.github.io/mongo-csharp-driver/2.0/getting_started/quick_tour/
        public async Task<Value> GetValueAsync(string Id)
        {
            FilterDefinition<Value> filter = "{Id: '" + Id + "'}"; // ?????????????????????? (worth a try!!)
            Value rv = await values.Find<Value>(new BsonDocument()).FirstAsync();
            return rv;
        }

        public bool RemoveValue(string Id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateValue(string Id, Value item)
        {
            throw new NotImplementedException();
        }
    }
}