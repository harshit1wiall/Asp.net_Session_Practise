using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoginPage.Models
{

    public class People
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

    }

}