using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace HebrewVerb.BlazorApp.Common.Extensions;

public static class CacheHelpers
{
    //public static async Task<IEnumerable<VerbModelDto>> GetVerbModelList(this IDistributedCache cache, IMediator mediator)
    //{
    //    var list = await cache.GetValueByKeyAsync<IEnumerable<VerbModelDto>>(VerbModelList);
    //    if (list == null)
    //    {
    //        list = await FilterOptions.GetVerbModels(mediator);
    //        await cache.SetValueByKeyAsync(VerbModelList, list);
    //    }
    //    return list;
    //}

    private static async Task<T?> GetValueByKeyAsync<T>(this IDistributedCache cache, string key)
    {
        var list = await cache.GetStringAsync(key);
        return list == null
            ? default
            : JsonSerializer.Deserialize<T>(list);
    }

    public static Task SetValueByKeyAsync<T>(this IDistributedCache cache, string key, T value) =>
        cache.SetStringAsync(key, JsonSerializer.Serialize(value));


}
