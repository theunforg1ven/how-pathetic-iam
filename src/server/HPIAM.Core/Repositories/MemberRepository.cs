using HPIAM.Core.Interfaces;
using HPIAM.Domain.Entities;
using HPIAM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HPIAM.Core.Repositories;

public class MemberRepository(DataContext context) : IMemberRepository
{
    public async Task<Member?> GetMemberByIdAsync(string id)
    {
        var member = await context.Members.FindAsync(id); ;

        return member;
    }

    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    {
        var members = await context.Members.ToListAsync();

        return await context.Members.ToListAsync();
    }

    public async Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(string memberId)
    {
        var photos = await context.Members
            .Where(m => m.Id == memberId)
            .SelectMany(m => m.Photos)
            .ToListAsync();

        return photos;
    }

    public async Task<bool> SaveAllAsync() 
        => await context.SaveChangesAsync() > 0;

    public void Update(Member member)
        => context.Entry(member).State = EntityState.Modified;
}
