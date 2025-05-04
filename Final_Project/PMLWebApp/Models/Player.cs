using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace PMLWebApp.Models
{
    public class Player
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        public string Player_ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Rating { get; set; }
        public string Team_Name { get; set; }
        public string Team_ID { get; set; }
    }
}