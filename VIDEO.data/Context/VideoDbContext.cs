using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VIDEO.Membership.data.Entities;

namespace VIDEO.Membership.data.Context;

public class VideoDbContext : DbContext
{
    public VideoDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var relation in builder.Model.GetEntityTypes()
            .SelectMany(e => e
            .GetForeignKeys()))
        {
            relation.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    public DbSet<Film> Films => Set<Film>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Director> Directors => Set<Director>();
}
