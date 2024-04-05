using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Images;
using KabeGami.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace KabeGami.Infrastructure.Persistence;
internal sealed class ApplicationDbContext
    : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor = null!;

    public DbSet<Image> Images { get; set; } = null!;

    public ApplicationDbContext() { }
    public ApplicationDbContext(PublishDomainEventsInterceptor publishDomainEventsInterceptor)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // WORKS ON EF
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=KabeGami;Trusted_Connection=True");
        }
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            //.Ignore<List<DomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
