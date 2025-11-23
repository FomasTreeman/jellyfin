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

    /// <summary>
    /// Gets or sets the custom maximum bitrate in bits per second.
    /// Only used when OptimizationPreset is Custom.
    /// </summary>
    public int? CustomMaxBitrate { get; set; }

    /// <summary>
    /// Gets or sets the custom video codec.
    /// Only used when OptimizationPreset is Custom.
    /// </summary>
    public string? CustomVideoCodec { get; set; }

    /// <summary>
    /// Gets or sets the custom audio codec.
    /// Only used when OptimizationPreset is Custom.
    /// </summary>
    public string? CustomAudioCodec { get; set; }

    /// <summary>
    /// Gets or sets the custom maximum width.
    /// Only used when OptimizationPreset is Custom.
    /// </summary>
    public int? CustomMaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the custom maximum height.
    /// Only used when OptimizationPreset is Custom.
    /// </summary>
    public int? CustomMaxHeight { get; set; }
}
