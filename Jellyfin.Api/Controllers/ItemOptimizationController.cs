using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Jellyfin.Api.Attributes;
using Jellyfin.Data.Dtos;
using Jellyfin.Data.Enums;
using Jellyfin.Server.Implementations.Item;
using MediaBrowser.Common.Api;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jellyfin.Api.Controllers;

/// <summary>
/// Item Optimization Controller.
/// </summary>
[Authorize(Policy = Policies.Download)]
public class ItemOptimizationController : BaseJellyfinApiController
{
    private readonly ItemOptimizationManager _optimizationManager;
    private readonly ILibraryManager _libraryManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemOptimizationController"/> class.
    /// </summary>
    /// <param name="optimizationManager">Instance of <see cref="ItemOptimizationManager"/>.</param>
    /// <param name="libraryManager">Instance of <see cref="ILibraryManager"/>.</param>
    public ItemOptimizationController(
        ItemOptimizationManager optimizationManager,
        ILibraryManager libraryManager)
    {
        _optimizationManager = optimizationManager;
        _libraryManager = libraryManager;
    }

    /// <summary>
    /// Gets optimization settings for an item.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <response code="200">Optimization settings retrieved.</response>
    /// <response code="404">Item optimization settings not found.</response>
    /// <returns>An <see cref="OkResult"/> containing the optimization settings.</returns>
    [HttpGet("Items/{itemId}/OptimizationSettings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemOptimizationSettingsDto>> GetOptimizationSettings(
        [FromRoute, Required] Guid itemId)
    {
        var settings = await _optimizationManager.GetOptimizationSettingsAsync(itemId).ConfigureAwait(false);
        if (settings == null)
        {
            return NotFound();
        }

        return settings;
    }

    /// <summary>
    /// Sets optimization settings for an item.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="dto">The optimization settings.</param>
    /// <response code="204">Optimization settings updated.</response>
    /// <returns>A <see cref="NoContentResult"/>.</returns>
    [HttpPost("Items/{itemId}/OptimizationSettings")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> SetOptimizationSettings(
        [FromRoute, Required] Guid itemId,
        [FromBody, Required] ItemOptimizationSettingsDto dto)
    {
        dto.ItemId = itemId;
        await _optimizationManager.SetOptimizationSettingsAsync(dto).ConfigureAwait(false);
        return NoContent();
    }

    /// <summary>
    /// Deletes optimization settings for an item.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <response code="204">Optimization settings deleted.</response>
    /// <returns>A <see cref="NoContentResult"/>.</returns>
    [HttpDelete("Items/{itemId}/OptimizationSettings")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteOptimizationSettings(
        [FromRoute, Required] Guid itemId)
    {
        await _optimizationManager.DeleteOptimizationSettingsAsync(itemId).ConfigureAwait(false);
        return NoContent();
    }

    /// <summary>
    /// Gets effective optimization settings for an item, including inherited settings.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="parentIds">Optional parent item ids (for inheritance).</param>
    /// <response code="200">Effective optimization settings retrieved.</response>
    /// <response code="404">No optimization settings found.</response>
    /// <returns>An <see cref="OkResult"/> containing the effective optimization settings.</returns>
    [HttpGet("Items/{itemId}/EffectiveOptimizationSettings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemOptimizationSettingsDto>> GetEffectiveOptimizationSettings(
        [FromRoute, Required] Guid itemId,
        [FromQuery] Guid[] parentIds)
    {
        parentIds ??= Array.Empty<Guid>();
        var settings = await _optimizationManager.GetEffectiveOptimizationSettingsAsync(itemId, parentIds).ConfigureAwait(false);
        if (settings == null)
        {
            return NotFound();
        }

        return settings;
    }

    /// <summary>
    /// Gets download parameters for an optimized item download.
    /// This returns the quality parameters that should be used when transcoding for download.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <response code="200">Download parameters retrieved.</response>
    /// <response code="404">Item not found.</response>
    /// <returns>An <see cref="OkResult"/> containing the download parameters.</returns>
    [HttpGet("Items/{itemId}/OptimizedDownloadInfo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OptimizedDownloadInfo>> GetOptimizedDownloadInfo(
        [FromRoute, Required] Guid itemId)
    {
        var item = _libraryManager.GetItemById(itemId);
        if (item == null)
        {
            return NotFound();
        }

        // Get parent IDs for inheritance
        var parentIds = GetParentIds(item);

        // Get effective optimization settings
        var settings = await _optimizationManager.GetEffectiveOptimizationSettingsAsync(itemId, parentIds).ConfigureAwait(false);

        var preset = settings?.OptimizationPreset ?? OptimizationPreset.Original;

        return new OptimizedDownloadInfo
        {
            ItemId = itemId,
            OptimizationPreset = preset,
            MaxBitrate = OptimizationPresetHelper.GetMaxBitrate(preset),
            VideoCodec = OptimizationPresetHelper.GetVideoCodec(preset),
            AudioCodec = OptimizationPresetHelper.GetAudioCodec(preset),
            MaxWidth = OptimizationPresetHelper.GetMaxWidth(preset),
            MaxHeight = OptimizationPresetHelper.GetMaxHeight(preset),
            RequiresTranscoding = preset != OptimizationPreset.Original
        };
    }

    private static Guid[] GetParentIds(BaseItem item)
    {
        var parentIds = new System.Collections.Generic.List<Guid>();

        // For episodes, include season and series
        if (item is Episode episode)
        {
            if (episode.Season != null)
            {
                parentIds.Add(episode.Season.Id);
            }

            if (episode.Series != null)
            {
                parentIds.Add(episode.Series.Id);
            }
        }

        // For seasons, include series
        else if (item is Season season)
        {
            if (season.Series != null)
            {
                parentIds.Add(season.Series.Id);
            }
        }

        return parentIds.ToArray();
    }
}
