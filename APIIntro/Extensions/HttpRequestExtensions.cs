using Microsoft.AspNetCore.Http;

using System.Net;

namespace APIIntro.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool IsLocal(this HttpRequest req)
        {
            var connection = req.HttpContext.Connection;
            if (connection.RemoteIpAddress != null)
            {
                return IPAddress.IsLoopback(connection.RemoteIpAddress);
            }
            return connection.RemoteIpAddress == null && connection.LocalIpAddress == null;
        }
    }
}
