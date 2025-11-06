using HPIAM.Domain.Entities;
using HPIAM.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPIAM.Core.Controllers;

public class MembersController(DataContext context) : BaseApiController // localhost:5001/api/members
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
    {
        var members =  await context.Users.ToListAsync();
        return members;
    }

    [Authorize]
    [HttpGet("{id}")] // localhost:5001/api/members/user-id
    public async Task<ActionResult<AppUser>> GetMember(string id)
    {
        var member =  await context.Users.FindAsync(id);

        if(member is null)
            return NotFound();

        return member;
    }
}
