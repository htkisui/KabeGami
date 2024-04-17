using KabeGami.Domain.Common.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KabeGami.Infrastructure.Persistence.Configurations;
internal class DomainEventConfigurations
    : IEntityTypeConfiguration<DomainEvent>
{
    public void Configure(EntityTypeBuilder<DomainEvent> builder)
    {
        builder.ToTable("DomainEvents");

        builder.HasKey(d => d.Guid);

        builder
            .Property(g => g.Guid)
            .ValueGeneratedNever();
    }
}
