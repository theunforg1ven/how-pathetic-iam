using HPIAM.Domain.Entities;
using HPIAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPIAM.Core.Controllers;

public class UsersController : BaseApiController
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet] // api/users
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() 
    {
        var users = await _context.Users.ToListAsync();
        
        return users;
    }

    [HttpGet("{id}")] // api/users/id
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        return user;
    }
}
