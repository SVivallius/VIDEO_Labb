using System.Diagnostics.CodeAnalysis;

namespace VIDEO.Membership.data.Entities;
public class Genre : IEntity
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; }
    [AllowNull]
    public virtual ICollection<Film> Films { get; set; }
}
