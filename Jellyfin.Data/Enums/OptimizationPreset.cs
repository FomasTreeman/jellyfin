namespace Jellyfin.Data.Enums;

/// <summary>
/// Enum representing mobile/tablet optimization presets for downloads.
/// </summary>
public enum OptimizationPreset
{
    /// <summary>
    /// No optimization - download original file.
    /// </summary>
    None = 0,

    /// <summary>
    /// Mobile quality preset - optimized for mobile devices (lower resolution and bitrate).
    /// </summary>
    Mobile = 1,

    /// <summary>
    /// Tablet quality preset - optimized for tablet devices (medium resolution and bitrate).
    /// </summary>
    Tablet = 2,

    /// <summary>
    /// Custom quality preset - user-defined settings.
    /// </summary>
    Custom = 3
}
