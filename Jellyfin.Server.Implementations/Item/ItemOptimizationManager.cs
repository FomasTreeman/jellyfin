using System;
using System.Linq;
using System.Threading.Tasks;
using Jellyfin.Data.Dtos;
using Jellyfin.Data.Enums;
using Jellyfin.Database.Implementations;
using Jellyfin.Database.Implementations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jellyfin.Server.Implementations.Item;

/// <summary>
/// Manages item optimization settings.
/// </summary>
public class ItemOptimizationManager
{
    private readonly IDbContextFactory<JellyfinDbContext> _dbContextFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemOptimizationManager"/> class.
    /// </summary>
    /// <param name="dbContextFactory">The database context factory.</param>
    public ItemOptimizationManager(IDbContextFactory<JellyfinDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Gets the optimization settings for an item.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <returns>The optimization settings or null if not set.</returns>
    public async Task<ItemOptimizationSettingsDto?> GetOptimizationSettingsAsync(Guid itemId)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (dbContext.ConfigureAwait(false))
        {
            var settings = await dbContext.ItemOptimizationSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ItemId.Equals(itemId))
                .ConfigureAwait(false);

            if (settings == null)
            {
                return null;
            }

            return new ItemOptimizationSettingsDto
            {
                ItemId = settings.ItemId,
                OptimizationPreset = (OptimizationPreset)settings.OptimizationPreset,
                CustomMaxBitrate = settings.CustomMaxBitrate,
                CustomVideoCodec = settings.CustomVideoCodec,
                CustomAudioCodec = settings.CustomAudioCodec,
                CustomMaxWidth = settings.CustomMaxWidth,
                CustomMaxHeight = settings.CustomMaxHeight
            };
        }
    }

    /// <summary>
    /// Sets the optimization settings for an item.
    /// </summary>
    /// <param name="dto">The optimization settings.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task SetOptimizationSettingsAsync(ItemOptimizationSettingsDto dto)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (dbContext.ConfigureAwait(false))
        {
            var settings = await dbContext.ItemOptimizationSettings
                .FirstOrDefaultAsync(s => s.ItemId.Equals(dto.ItemId))
                .ConfigureAwait(false);

            if (settings == null)
            {
                settings = new ItemOptimizationSettings(dto.ItemId);
                dbContext.ItemOptimizationSettings.Add(settings);
            }

            settings.OptimizationPreset = (int)dto.OptimizationPreset;
            settings.CustomMaxBitrate = dto.CustomMaxBitrate;
            settings.CustomVideoCodec = dto.CustomVideoCodec;
            settings.CustomAudioCodec = dto.CustomAudioCodec;
            settings.CustomMaxWidth = dto.CustomMaxWidth;
            settings.CustomMaxHeight = dto.CustomMaxHeight;

            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Deletes the optimization settings for an item.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task DeleteOptimizationSettingsAsync(Guid itemId)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (dbContext.ConfigureAwait(false))
        {
            var settings = await dbContext.ItemOptimizationSettings
                .FirstOrDefaultAsync(s => s.ItemId.Equals(itemId))
                .ConfigureAwait(false);

            if (settings != null)
            {
                dbContext.ItemOptimizationSettings.Remove(settings);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Gets the effective optimization settings for an item, including inherited settings from parent items.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="parentIds">The parent item ids (season, series, etc.).</param>
    /// <returns>The effective optimization settings or null if not set.</returns>
    public async Task<ItemOptimizationSettingsDto?> GetEffectiveOptimizationSettingsAsync(Guid itemId, Guid[] parentIds)
    {
        // First check the item itself
        var settings = await GetOptimizationSettingsAsync(itemId).ConfigureAwait(false);
        if (settings != null && settings.OptimizationPreset != OptimizationPreset.None)
        {
            return settings;
        }

        // Then check parent items (e.g., season, series)
        foreach (var parentId in parentIds)
        {
            settings = await GetOptimizationSettingsAsync(parentId).ConfigureAwait(false);
            if (settings != null && settings.OptimizationPreset != OptimizationPreset.None)
            {
                return settings;
            }
        }

        return null;
    }
}
