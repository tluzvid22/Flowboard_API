using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Helpers
{
    public static class FluentExtensions
    {
        public static IResult ToBadRequest(this IEnumerable<IError> errors)
        {
            var problemDetails = errors.Select(error => error.Metadata["ProblemDetails"] as ProblemDetails).ToList();

            if (problemDetails.Count is 1)
                return Results.Problem(problemDetails.First()!);

            return Results.Problem(
                title: "Multiple problems occurred",
                detail: "There were multiple problems occurring when processing the request. See problems list for details.",
                type: "/errors/multiple",
                statusCode: StatusCodes.Status400BadRequest,
                extensions: new Dictionary<string, object> { { "Problems", problemDetails.ToArray() } }!
            );
        }
    }

}
