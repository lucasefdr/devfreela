using DevFreela.Application.Common;
using System.Text.Json;

namespace DevFreela.API.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeaders<T>(this HttpResponse response, PagedResult<T> pagedResult)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var metadata = new
        {
            pagedResult.TotalCount,
            pagedResult.PageSize,
            pagedResult.CurrentPage,
            pagedResult.TotalPages,
            pagedResult.HasNext,
            pagedResult.HasPrevious
        };


        response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata, jsonOptions));
        response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
    }
}
