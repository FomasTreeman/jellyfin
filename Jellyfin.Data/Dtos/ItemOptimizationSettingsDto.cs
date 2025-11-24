using System;
using Jellyfin.Data.Enums;

namespace Jellyfin.Data.Dtos;

/// <summary>
/// A dto representing optimization settings for an item.
/// </summary>
public class ItemOptimizationSettingsDto
{
    /// <summary>
    /// Gets or sets the item id.
    /// </summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Gets or sets the optimization preset.
    /// </summary>
    public OptimizationPreset OptimizationPreset { get; set; }
}
