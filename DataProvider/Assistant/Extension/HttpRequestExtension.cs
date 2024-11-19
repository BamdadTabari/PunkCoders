using Base.Assistant.Models;
using Microsoft.AspNetCore.Http;

namespace Base.Assistant.Extension;

public static class HttpRequestExtension
{
    // to save in log 
    private static int GetUserId(this HttpRequest request)
    {
        return request.GetUserId();
    }
    // get ip address for save in log
    private static string GetIpAddress(this HttpRequest request)
    {
        return request?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";
    }
    // for save in log
    private static string GetRequestBody(this HttpRequest request)
    {
        return request?.HttpContext?.Request.Body.ToString() ?? "";
    }

    public static RequestInfo GetRequestInfo(this HttpRequest request)
    {
        return new RequestInfo
        {
            UserId = request.GetUserId(),
            IpAddress = request.GetIpAddress(),
            RequestBody = request.GetRequestBody()
        };
    }
}