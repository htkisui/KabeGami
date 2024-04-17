using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Galleries;
using KabeGami.Domain.Images;
using KabeGami.Domain.KabeGamis;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KabeGami.Infrastructure.Persistence;
internal sealed class ApplicationDbContext
    : DbContext
{
    private readonly IPublisher _publisher = null!;

    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<KabeGamiCore> KabeGamiCores { get; set; }

    public ApplicationDbContext() { }

    public ApplicationDbContext(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // WORKS ON EF
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=KabeGami;Trusted_Connection=True");
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<IReadOnlyList<DomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Create Unique KabeGami Entity
        var kabeGamiCore = KabeGamiCore.Create();
        modelBuilder.Entity<KabeGamiCore>()
            .HasData(kabeGamiCore);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entitiesWithDomainEvents = ChangeTracker.Entries<IHasDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        var domainEvents = entitiesWithDomainEvents.SelectMany(e => e.DomainEvents).ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        entitiesWithDomainEvents.ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
