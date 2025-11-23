using Jellyfin.Data.Enums;

namespace Jellyfin.Server.Implementations.Item;

/// <summary>
/// Provides quality presets for download optimization.
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
            OptimizationPreset.Original => int.MaxValue,
            OptimizationPreset.High => 10_000_000, // 10 Mbps for high quality
            OptimizationPreset.Medium => 5_000_000, // 5 Mbps for medium quality
            OptimizationPreset.Low => 2_500_000, // 2.5 Mbps for low quality
            OptimizationPreset.Mobile => 1_500_000, // 1.5 Mbps for mobile
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
            OptimizationPreset.Original => string.Empty,
            OptimizationPreset.High => "h264",
            OptimizationPreset.Medium => "h264",
            OptimizationPreset.Low => "h264",
            OptimizationPreset.Mobile => "h264",
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
            OptimizationPreset.Original => string.Empty,
            OptimizationPreset.High => "aac",
            OptimizationPreset.Medium => "aac",
            OptimizationPreset.Low => "aac",
            OptimizationPreset.Mobile => "aac",
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
            OptimizationPreset.Original => null,
            OptimizationPreset.High => 1920, // 1080p for high quality
            OptimizationPreset.Medium => 1280, // 720p for medium quality
            OptimizationPreset.Low => 854, // 480p for low quality
            OptimizationPreset.Mobile => 640, // 360p for mobile
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
            OptimizationPreset.Original => null,
            OptimizationPreset.High => 1080, // 1080p for high quality
            OptimizationPreset.Medium => 720, // 720p for medium quality
            OptimizationPreset.Low => 480, // 480p for low quality
            OptimizationPreset.Mobile => 360, // 360p for mobile
            _ => null
        };
    }
}
