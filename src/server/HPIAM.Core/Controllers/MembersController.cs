using HPIAM.Core.Interfaces;
using HPIAM.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HPIAM.Core.Controllers;

[Authorize]
public class MembersController(IMemberRepository memberRepository) : BaseApiController // localhost:5001/api/members
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
    {
        var members =  await memberRepository.GetMembersAsync();
        return Ok(members);
    }

    [HttpGet("{id}")] // localhost:5001/api/members/user-id
    public async Task<ActionResult<Member>> GetMember(string id)
    {
        var member =  await memberRepository.GetMemberByIdAsync(id);

        if(member is null)
            return NotFound();

        return member;
    }

    [HttpGet("{id}/photos")]
    public async Task<ActionResult<IReadOnlyList<Photo>>> GetPhotosForMember(string id)
    {
        var photos =  await memberRepository.GetPhotosForMemberAsync(id);
        return Ok(photos);
    }
}
