using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class BookService
{
    private readonly IMongoCollection<Book> _booksCollection;
    
    // public BookService(IMongoCollection<Book> booksCollection)
    public BookService(BookStoreDatabaseSettings mongoSettings)
    {
        var mongoClient = new MongoClient(mongoSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoSettings.DatabaseName);
        var booksCollection = mongoDatabase.GetCollection<Book>(mongoSettings.BooksCollectionName);
        _booksCollection = booksCollection;
    }
    
    public Task<List<Book>> GetAsyncList() =>
         _booksCollection.Find(_ => true).ToListAsync();

    public Task<Book?> GetAsync(string id) =>
         _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public Task CreateAsync(Book newBook) =>
         _booksCollection.InsertOneAsync(newBook);

    public Task UpdateAsync(string id, Book updatedBook) => 
        _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public Task RemoveAsync(string id) =>
        _booksCollection.DeleteOneAsync(x => x.Id == id);
}