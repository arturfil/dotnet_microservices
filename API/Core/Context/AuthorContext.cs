using API.Core.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Core.Context;

public class AuthorContext : IAuthorContext {
  
  private readonly IMongoDatabase _db;
  public AuthorContext(IOptions<MongoSettings> opt) {
    var client = new MongoClient(opt.Value.ConnectionString);
    _db = client.GetDatabase(opt.Value.Database);  
  }

  public IMongoCollection<Author> Authors => _db.GetCollection<Author>("Author");

}