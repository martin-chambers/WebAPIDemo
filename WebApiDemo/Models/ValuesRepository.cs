using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Configuration;

namespace WebApiDemo.Models
{
    public class ValuesRepository : IValuesRepository
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<Value> values;

        // useful resources:
        // http://mongodb.github.io/mongo-csharp-driver/2.0/getting_started/quick_tour/
        // http://dotnetcodr.com/data-storage/

        public ValuesRepository()
        {
            string connection = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
            client = new MongoClient(connection);
            database = client.GetDatabase("testDB");
            values = database.GetCollection<Value>("values");
        }
        public async Task AddValueAsync(Value item)
        {
            item.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();            
            await values.InsertOneAsync(item);            
        }

        public async Task<IEnumerable<Value>> GetAllValuesAsync()
        {
            List<Value> valueList = await values.Find(new BsonDocument()).ToListAsync();
            return valueList;            
        }
        public async Task<Value> GetValueAsync(string Id)
        {
            var filter = Builders<Value>.Filter.Eq("Id", Id);
            Value rv = await values.Find(filter).FirstAsync();
            return rv;
        }
        public async Task<DeleteResult> RemoveValue(string Id)
        {
            var filter = Builders<Value>.Filter.Eq("Id", Id);
            return await values.DeleteOneAsync(filter);            
        }
        public bool UpdateValue(string Id, Value item)
        {
            throw new NotImplementedException();
        }
    }
}