using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // http://localhost:50001/api/users - if there are not hard-coded then a GET is looked for on this path
// interesting note about scope - once the http request is process and the response is returned, the context is disposed of
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    // we made this async because it is best practice to offload these types of database
    // queries to a delegate
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();

        return users; // the framework will return the correct http response in this case
    }

    [HttpGet("{id}")] // /api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        return await _context.Users.FindAsync(id);
        // note that the context will track the entity and return if found, otherwise it will
        // query the database, then update the context and return the data
    }
}
