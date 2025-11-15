using System.Text.Json.Serialization;

namespace HPIAM.Domain.Entities;

public class Photo
{
    public int Id { get; set; }
    
    public required string Url { get; set; }
    
    public string? PublicId { get; set; }

    // public bool IsMain { get; set; }

    // Navigation property
    public string MemberId { get; set; } = null!;

    [JsonIgnore]
    public Member Member { get; set; } = null!;
}
