using HPIAM.Domain.Entities;
using HPIAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPIAM.Core.Controllers;

[ApiController]
[Route("api/[controller]")] // localhost:5001/api/members
public class MembersController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
    {
        var members =  await context.Users.ToListAsync();
        return members;
    }

    [HttpGet("{id}")] // localhost:5001/api/members/user-id
    public async Task<ActionResult<AppUser>> GetMember(string id)
    {
        var member =  await context.Users.FindAsync(id);

        if(member is null)
            return NotFound();

        return member;
    }
}
