using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace VIDEO.Membership.data.Entities;
public class Film : IEntity
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string Title { get; set; }
    [MaxLength(1024), AllowNull]
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    [MaxLength(255), Required]
    public string Url { get; set; }
    [AllowNull]
    public int DirectorId { get; set; }
    [AllowNull]
    public virtual Director Director { get; set; }
    [AllowNull]
    public virtual ICollection<Genre> Genres { get; set; }
    public bool free { get; set; }
}
