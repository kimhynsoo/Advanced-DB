using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PMLWebApp.Models
{
    public class Team
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        public string Team_ID { get; set; }
        public string Team_Name { get; set; }
        public string Short_Name { get; set; }
        public int Founded_Year { get; set; }
        public string Stadium { get; set; }
    }
}