using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ResumeSmart.Api.DB.Entities;

public class Users
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonRequired]
    public required string Name { get; set; }
    
    [BsonRequired]
    public required string Email { get; set; }
    
    [BsonRequired]
    public required string Password { get; set; }
}