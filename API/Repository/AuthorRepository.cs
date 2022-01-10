using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Context;
using API.Core.Entities;
using MongoDB.Driver;

namespace Repostiroy;

public class AuthorRepository : IAuthorRepository {
  private readonly IAuthorContext _context;

  public AuthorRepository(IAuthorContext context) {
      _context = context;
  }

  public async Task<IEnumerable<Author>> GetAuthors() {
    return await _context.Authors.Find(p => true).ToListAsync();
  }

}