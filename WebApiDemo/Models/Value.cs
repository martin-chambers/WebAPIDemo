//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiDemo.Models
{
    public class Value
    {
        //public Value(string _name, int _age)
        //{
        //    Name = _name;
        //    Age = _age;
        //}

        // regarding the MongoDB-related attributes ...
        // in theory domain objects such as this should not know about the data persistence
        // model elsewhere in the solution, but this is a learning project and my priority is
        // to get it working. Worth considering a level of indirection in a production
        // solution: e.g. an injectable decorator class or some such ...
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Name { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int Age { get; set; }        
    }
}