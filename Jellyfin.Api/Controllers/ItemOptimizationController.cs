using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Jellyfin.Api.Attributes;
using Jellyfin.Data.Dtos;
using Jellyfin.Server.Implementations.Item;
using MediaBrowser.Common.Api;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemOptimizationController"/> class.
    /// </summary>
    /// <param name="optimizationManager">Instance of <see cref="ItemOptimizationManager"/>.</param>
    public ItemOptimizationController(ItemOptimizationManager optimizationManager)
    {
        _optimizationManager = optimizationManager;
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
}
