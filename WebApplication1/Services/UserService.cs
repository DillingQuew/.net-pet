using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class UserService
{
    private readonly IMongoCollection<User> _usersCollection;
    
    // public UserService(IMongoCollection<User> usersCollection)
    public UserService(BookStoreDatabaseSettings mongoSettings)
    {
        var mongoClient = new MongoClient(mongoSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoSettings.DatabaseName);
        var usersCollection = mongoDatabase.GetCollection<User>(mongoSettings.UsersCollectionName);
        _usersCollection = usersCollection;
    }
    
    public Task<List<User>> GetListAsync() => 
        _usersCollection.Find(_ => true).ToListAsync();

    public Task<User?> GetAsync(string id) =>
         _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public Task CreateAsync(User newUser) =>
         _usersCollection.InsertOneAsync(newUser);

    public Task UpdateAsync(string id, User updatedUser) =>
         _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public Task RemoveAsync(string id) =>
         _usersCollection.DeleteOneAsync(x => x.Id == id);
}