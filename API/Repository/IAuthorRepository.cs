using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Entities;

namespace Repostiroy;

public interface IAuthorRepository {

  Task<IEnumerable<Author>> GetAuthors();

}