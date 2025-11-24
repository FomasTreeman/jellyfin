using Jellyfin.Data.Enums;
using Jellyfin.Server.Implementations.Item;
using Xunit;

namespace Jellyfin.Server.Implementations.Tests.Item;

public class OptimizationPresetHelperTests
{
    [Theory]
    [InlineData(OptimizationPreset.Original, int.MaxValue)]
    [InlineData(OptimizationPreset.High, 10_000_000)]
    [InlineData(OptimizationPreset.Medium, 5_000_000)]
    [InlineData(OptimizationPreset.Low, 2_500_000)]
    [InlineData(OptimizationPreset.Mobile, 1_500_000)]
    public void GetMaxBitrate_ReturnsCorrectBitrate(OptimizationPreset preset, int expectedBitrate)
    {
        var result = OptimizationPresetHelper.GetMaxBitrate(preset);

        Assert.Equal(expectedBitrate, result);
    }

    [Theory]
    [InlineData(OptimizationPreset.Original, "")]
    [InlineData(OptimizationPreset.High, "h264")]
    [InlineData(OptimizationPreset.Medium, "h264")]
    [InlineData(OptimizationPreset.Low, "h264")]
    [InlineData(OptimizationPreset.Mobile, "h264")]
    public void GetVideoCodec_ReturnsCorrectCodec(OptimizationPreset preset, string expectedCodec)
    {
        var result = OptimizationPresetHelper.GetVideoCodec(preset);

        Assert.Equal(expectedCodec, result);
    }

    [Theory]
    [InlineData(OptimizationPreset.Original, "")]
    [InlineData(OptimizationPreset.High, "aac")]
    [InlineData(OptimizationPreset.Medium, "aac")]
    [InlineData(OptimizationPreset.Low, "aac")]
    [InlineData(OptimizationPreset.Mobile, "aac")]
    public void GetAudioCodec_ReturnsCorrectCodec(OptimizationPreset preset, string expectedCodec)
    {
        var result = OptimizationPresetHelper.GetAudioCodec(preset);

        Assert.Equal(expectedCodec, result);
    }

    [Theory]
    [InlineData(OptimizationPreset.Original, null)]
    [InlineData(OptimizationPreset.High, 1920)]
    [InlineData(OptimizationPreset.Medium, 1280)]
    [InlineData(OptimizationPreset.Low, 854)]
    [InlineData(OptimizationPreset.Mobile, 640)]
    public void GetMaxWidth_ReturnsCorrectWidth(OptimizationPreset preset, int? expectedWidth)
    {
        var result = OptimizationPresetHelper.GetMaxWidth(preset);

        Assert.Equal(expectedWidth, result);
    }

    [Theory]
    [InlineData(OptimizationPreset.Original, null)]
    [InlineData(OptimizationPreset.High, 1080)]
    [InlineData(OptimizationPreset.Medium, 720)]
    [InlineData(OptimizationPreset.Low, 480)]
    [InlineData(OptimizationPreset.Mobile, 360)]
    public void GetMaxHeight_ReturnsCorrectHeight(OptimizationPreset preset, int? expectedHeight)
    {
        var result = OptimizationPresetHelper.GetMaxHeight(preset);

        Assert.Equal(expectedHeight, result);
    }
}
