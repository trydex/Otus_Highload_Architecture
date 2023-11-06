using Host.Controllers.Generated;

namespace Host.Helpers;

public static class ActionResults
{
    private static readonly IReadOnlyDictionary<string, IEnumerable<string>> DefaultHeaders =
        new Dictionary<string, IEnumerable<string>>();
    public static SwaggerResponse<TResult> Success<TResult>(TResult result) => new(200, DefaultHeaders, result);
    public static SwaggerResponse<TResult> Success<TResult>() where TResult : class => new(200, DefaultHeaders, null!);
    public static SwaggerResponse<TResult> BadRequest<TResult>(TResult result) => new(400, DefaultHeaders, result);
    public static SwaggerResponse<TResult> BadRequest<TResult>() where TResult : class => new(400, DefaultHeaders, null!);
    public static SwaggerResponse<TResult> NotFound<TResult>(TResult result) => new(404, DefaultHeaders, result);
    public static SwaggerResponse<TResult> NotFound<TResult>() where TResult : class => new(404, DefaultHeaders, null!);
}