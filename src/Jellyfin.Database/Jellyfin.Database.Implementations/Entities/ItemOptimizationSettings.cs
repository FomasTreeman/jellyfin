using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jellyfin.Database.Implementations.Entities;

/// <summary>
/// An entity that represents optimization settings for an item.
/// </summary>
public class ItemOptimizationSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemOptimizationSettings"/> class.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    public ItemOptimizationSettings(Guid itemId)
    {
        ItemId = itemId;
        OptimizationPreset = 0; // None
    }

    /// <summary>
    /// Gets the id.
    /// </summary>
    /// <remarks>
    /// Required.
    /// </remarks>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    /// <summary>
    /// Gets or sets the id of the associated item.
    /// </summary>
    /// <remarks>
    /// Required.
    /// </remarks>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Gets or sets the optimization preset (0=None, 1=Mobile, 2=Tablet, 3=Custom).
    /// </summary>
    /// <remarks>
    /// Required.
    /// </remarks>
    public int OptimizationPreset { get; set; }

    /// <summary>
    /// Gets or sets the custom maximum bitrate in bits per second.
    /// Only used when OptimizationPreset is Custom.
    /// </summary>
    public int? CustomMaxBitrate { get; set; }

    /// <summary>
    /// Gets or sets the custom video codec.
    /// Only used when OptimizationPreset is Custom.
    /// </summary>
    [StringLength(64)]
    public string? CustomVideoCodec { get; set; }

    /// <summary>
    /// Gets or sets the custom audio codec.
    /// Only used when OptimizationPreset is Custom.
    /// </summary>
    [StringLength(64)]
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
