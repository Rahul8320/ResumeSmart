using MongoDB.Driver;
using ResumeSmart.Api.Configs;
using ResumeSmart.Api.DB.Entities;

namespace ResumeSmart.Api.DB;

public class MongoDbContext(IMongoDatabase database, ILogger<MongoDbContext> logger)
{
    public IMongoCollection<Users> Users => database.GetCollection<Users>(DbConstents.UserCollection);
    
    public void EnsureIndexes()
    {
        var userCollection = database.GetCollection<Users>(DbConstents.UserCollection);

        var indexKeys = Builders<Users>.IndexKeys.Ascending(user => user.Email);
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<Users>(indexKeys, indexOptions);

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