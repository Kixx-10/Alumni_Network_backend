using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Alumni.Hubs
{
    public class CustomUserIdProvider : IUserIdProvider  //.net core buit in interface for providing user identifiers in SignalR
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var customId = connection.User?.FindFirst("id")?.Value;
            if (!string.IsNullOrEmpty(customId))
            {
                return customId;
            }
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}