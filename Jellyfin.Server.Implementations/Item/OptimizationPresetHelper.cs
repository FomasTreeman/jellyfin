using Jellyfin.Data.Enums;

namespace Jellyfin.Server.Implementations.Item;

/// <summary>
/// Provides quality presets for mobile optimization.
/// </summary>
public static class OptimizationPresetHelper
{
    /// <summary>
    /// Gets the maximum bitrate for a given preset in bits per second.
    /// </summary>
    /// <param name="preset">The optimization preset.</param>
    /// <returns>The maximum bitrate in bits per second.</returns>
    public static int GetMaxBitrate(OptimizationPreset preset)
    {
        return preset switch
        {
            OptimizationPreset.Mobile => 2_000_000, // 2 Mbps for mobile
            OptimizationPreset.Tablet => 4_000_000, // 4 Mbps for tablet
            OptimizationPreset.None => int.MaxValue,
            OptimizationPreset.Custom => int.MaxValue,
            _ => int.MaxValue
        };
    }

    /// <summary>
    /// Gets the video codec for a given preset.
    /// </summary>
    /// <param name="preset">The optimization preset.</param>
    /// <returns>The video codec.</returns>
    public static string GetVideoCodec(OptimizationPreset preset)
    {
        return preset switch
        {
            OptimizationPreset.Mobile => "h264",
            OptimizationPreset.Tablet => "h264",
            OptimizationPreset.None => string.Empty,
            OptimizationPreset.Custom => string.Empty,
            _ => string.Empty
        };
    }

    /// <summary>
    /// Gets the audio codec for a given preset.
    /// </summary>
    /// <param name="preset">The optimization preset.</param>
    /// <returns>The audio codec.</returns>
    public static string GetAudioCodec(OptimizationPreset preset)
    {
        return preset switch
        {
            OptimizationPreset.Mobile => "aac",
            OptimizationPreset.Tablet => "aac",
            OptimizationPreset.None => string.Empty,
            OptimizationPreset.Custom => string.Empty,
            _ => string.Empty
        };
    }

    /// <summary>
    /// Gets the maximum width for a given preset.
    /// </summary>
    /// <param name="preset">The optimization preset.</param>
    /// <returns>The maximum width in pixels.</returns>
    public static int? GetMaxWidth(OptimizationPreset preset)
    {
        return preset switch
        {
            OptimizationPreset.Mobile => 1280, // 720p width for mobile
            OptimizationPreset.Tablet => 1920, // 1080p width for tablet
            OptimizationPreset.None => null,
            OptimizationPreset.Custom => null,
            _ => null
        };
    }

    /// <summary>
    /// Gets the maximum height for a given preset.
    /// </summary>
    /// <param name="preset">The optimization preset.</param>
    /// <returns>The maximum height in pixels.</returns>
    public static int? GetMaxHeight(OptimizationPreset preset)
    {
        return preset switch
        {
            OptimizationPreset.Mobile => 720, // 720p for mobile
            OptimizationPreset.Tablet => 1080, // 1080p for tablet
            OptimizationPreset.None => null,
            OptimizationPreset.Custom => null,
            _ => null
        };
    }
}
