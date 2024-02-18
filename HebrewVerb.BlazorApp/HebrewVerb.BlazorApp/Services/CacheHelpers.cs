using HebrewVerb.Application.Models;
using HebrewVerb.Application.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using static HebrewVerb.BlazorApp.Common.Constants;

namespace HebrewVerb.BlazorApp.Services;

public static class CacheHelpers
{
    public static async Task<IEnumerable<GizraDto>> GetGizraList(this IDistributedCache cache, IMediator mediator)
    {
        var list = await GetValueByKeyAsync<IEnumerable<GizraDto>>(cache, GizraList);
        if (list == null)
        {
            list = await FilterService.GetGizras(mediator);
            await SetValueByKeyAsync(cache, GizraList, list);
        }
        return list;
    }

    public static async Task<IEnumerable<VerbModelDto>> GetVerbModelList(this IDistributedCache cache, IMediator mediator)
    {
        var list = await GetValueByKeyAsync<IEnumerable<VerbModelDto>>(cache, VerbModelList);
        if (list == null)
        {
            list = await FilterService.GetVerbModels(mediator);
            await SetValueByKeyAsync(cache, VerbModelList, list);
        }
        return list;
    }

    private static async Task<T?> GetValueByKeyAsync<T>(this IDistributedCache cache, string key)
    {
        var list = await cache.GetStringAsync(key);
        return list == null
            ? default
            : JsonSerializer.Deserialize<T>(list);
    }

    public static Task SetValueByKeyAsync<T>(this IDistributedCache cache, string key, T value) =>
        cache.SetStringAsync(key, JsonSerializer.Serialize<T>(value));


}
