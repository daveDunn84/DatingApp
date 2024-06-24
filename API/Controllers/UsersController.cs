using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        var users = _context.Users.ToList();

        return users; // the framework will return the correct http response in this case
    }

    [HttpGet("{id}")] // /api/users/2
    public ActionResult<AppUser> GetUser(int id)
    {
        return _context.Users.Find(id);
        // note that the context will track the entity and return if found, otherwise it will
        // query the database, then update the context and return the data
    }
}
