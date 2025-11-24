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
        OptimizationPreset = 0; // Original
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
    /// Gets or sets the optimization preset (0=Original, 1=High, 2=Medium, 3=Low, 4=Mobile).
    /// </summary>
    /// <remarks>
    /// Required.
    /// </remarks>
    public int OptimizationPreset { get; set; }
}
