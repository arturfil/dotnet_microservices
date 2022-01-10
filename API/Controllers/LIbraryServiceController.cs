using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Repostiroy;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryServiceController : ControllerBase {
    private readonly IAuthorRepository _authorContext;
  
  public LibraryServiceController(IAuthorRepository authorContext) {
      _authorContext = authorContext;

  }

  [HttpGet("authors")]
  public async Task<ActionResult<IEnumerable<Author>>> GetAuthors() {
    var authors = await _authorContext.GetAuthors();
    return Ok(authors);
  }

}