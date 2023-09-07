using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoginPage.Models;
public class Details
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? MyId { get; set; }
    public string? Username { get; set; }
    public string? Description { get; set; }
}
