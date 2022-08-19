using System.Net;

namespace Workshop.Tests.Integration;

public class BaseResponse<TResponse>
{
    public HttpStatusCode? StatusCode { get; set; }
    public string? Message { get; set; }
    public string? StackTrace  { get; set; }
    public TResponse? DeserializedResponse { get; set; }
    public bool Success { get; set; }
}