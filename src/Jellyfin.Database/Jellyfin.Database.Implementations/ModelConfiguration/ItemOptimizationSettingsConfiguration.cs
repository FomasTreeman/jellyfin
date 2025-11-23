using Jellyfin.Database.Implementations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jellyfin.Database.Implementations.ModelConfiguration;

/// <summary>
/// FluentAPI configuration for the ItemOptimizationSettings entity.
/// </summary>
public class ItemOptimizationSettingsConfiguration : IEntityTypeConfiguration<ItemOptimizationSettings>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ItemOptimizationSettings> builder)
    {
        builder.HasKey(d => d.Id);
        builder.HasIndex(d => d.ItemId).IsUnique();
    }
}
