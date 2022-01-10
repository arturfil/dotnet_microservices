using API.Core.Entities;
using MongoDB.Driver;

namespace API.Core.Context;

public interface IAuthorContext {

  IMongoCollection<Author> Authors { get; }

}