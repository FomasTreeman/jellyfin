namespace Jellyfin.Data.Enums;

/// <summary>
/// Enum representing optimization presets for downloads.
/// </summary>
public enum OptimizationPreset
{
    /// <summary>
    /// Original quality - download original file without optimization.
    /// </summary>
    Original = 0,

    /// <summary>
    /// High quality preset - high resolution and bitrate.
    /// </summary>
    High = 1,

    /// <summary>
    /// Medium quality preset - medium resolution and bitrate.
    /// </summary>
    Medium = 2,

    /// <summary>
    /// Low quality preset - lower resolution and bitrate for smaller file sizes.
    /// </summary>
    Low = 3,

    /// <summary>
    /// Mobile quality preset - optimized for mobile devices.
    /// </summary>
    Mobile = 4
}
