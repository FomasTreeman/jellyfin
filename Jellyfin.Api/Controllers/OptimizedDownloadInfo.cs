using System;
using Jellyfin.Data.Enums;

namespace Jellyfin.Api.Controllers;

/// <summary>
/// Contains information for optimized download parameters.
/// </summary>
public class OptimizedDownloadInfo
{
    /// <summary>
    /// Gets or sets the item id.
    /// </summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Gets or sets the optimization preset being used.
    /// </summary>
    public OptimizationPreset OptimizationPreset { get; set; }

    /// <summary>
    /// Gets or sets the maximum bitrate in bits per second.
    /// </summary>
    public int MaxBitrate { get; set; }

    /// <summary>
    /// Gets or sets the video codec.
    /// </summary>
    public string? VideoCodec { get; set; }

    /// <summary>
    /// Gets or sets the audio codec.
    /// </summary>
    public string? AudioCodec { get; set; }

    /// <summary>
    /// Gets or sets the maximum width in pixels.
    /// </summary>
    public int? MaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the maximum height in pixels.
    /// </summary>
    public int? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether transcoding is required.
    /// </summary>
    public bool RequiresTranscoding { get; set; }
}
