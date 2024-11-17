using KabeGami.Domain.Galleries;
using KabeGami.Domain.Homes;
using KabeGami.Domain.Images;
using Microsoft.EntityFrameworkCore;

namespace KabeGami.Infrastucture.Persistence;
internal sealed class ApplicationDbContext
    : DbContext
{
    public required DbSet<Gallery> Galleries { get; set; }
    public required DbSet<Home> Homes { get; set; }
    public required DbSet<Image> Images { get; set; }

    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // WORKS ON EF
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=KabeGami;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Create Unique Home Entity
        var home = Home.Create();
        modelBuilder.Entity<Home>()
            .HasData(home);

        base.OnModelCreating(modelBuilder);
    }
}
