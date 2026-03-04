using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ResumeSmart.Api.DB.Entities;

/// <summary>
/// Represents user entity
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets user ID
    /// </summary>
    [BsonId]
    public ObjectId Id { get; set; }
    
    /// <summary>
    /// Gets or sets user name
    /// </summary>
    [BsonRequired]
    public required string Name { get; set; }
    
    /// <summary>
    /// Gets or sets user email
    /// </summary>
    [BsonRequired]
    public required string Email { get; set; }
    
    /// <summary>
    /// Gets or sets password
    /// </summary>
    [BsonRequired]
    public required string Password { get; set; }
    
    /// <summary>
    /// Gets or sets created at value
    /// </summary>
    [BsonRequired]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public required DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets modified at value
    /// </summary>
    [BsonRequired]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public required DateTime ModifiedAt { get; set; }
}