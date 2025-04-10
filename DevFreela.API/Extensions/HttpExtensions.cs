using DevFreela.Application.Common;
using System.Text.Json;
using DevFreela.Core.Common;
using DevFreela.Core.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeaders<T>(this HttpResponse response, PagedResult<T> pagedResult)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
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

    public static IActionResult MapErrorToHttpResponse(this ControllerBase controller, Error error)
    {
        return error.Type switch
        {
            ErrorType.NotFound => controller.NotFound(new ProblemDetails
            {
                Title = "Resource not found",
                Detail = error.Description,
                Status = StatusCodes.Status404NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            }),
            ErrorType.Validation => controller.BadRequest(new ProblemDetails
            {
                Title = "Validation error",
                Detail = error.Description,
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            }),
            ErrorType.Conflict => controller.Conflict(new ProblemDetails
            {
                Title = "Conflict",
                Detail = error.Description,
                Status = StatusCodes.Status409Conflict,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8"
            }),
            _ => controller.StatusCode(500, new ProblemDetails
            {
                Title = "Internal server error",
                Detail = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            })
        };
    }

}