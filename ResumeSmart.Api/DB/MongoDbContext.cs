using MongoDB.Driver;
using ResumeSmart.Api.Configs;
using ResumeSmart.Api.DB.Entities;

namespace ResumeSmart.Api.DB;

/// <summary>
/// Initialize a mongo db context 
/// </summary>
/// <param name="database">Mongo database instance</param>
/// <param name="logger">Logger instance</param>
public class MongoDbContext(IMongoDatabase database, ILogger<MongoDbContext> logger)
{
    /// <summary>
    /// Holds users collection 
    /// </summary>
    public IMongoCollection<User> Users => database.GetCollection<User>(DbConstents.UserCollection);
    
    /// <summary>
    /// Creates required indexes in database
    /// </summary>
    public void EnsureIndexes()
    {
        var userCollection = database.GetCollection<User>(DbConstents.UserCollection);

        var indexKeys = Builders<User>.IndexKeys.Ascending(user => user.Email);
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<User>(indexKeys, indexOptions);

        try
        {
            userCollection.Indexes.CreateOne(indexModel);
            logger.LogInformation("Created Indexes for {collection}", DbConstents.UserCollection);
        }
        catch (MongoCommandException ex)
        {
            logger.LogError("Index creation failed: {@Message}", ex.Message);
        }
    }
}