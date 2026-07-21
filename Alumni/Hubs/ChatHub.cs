
using Alumni.DTOs;
using Alumni.Services.ChatService;
using Alumni.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Alumni.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IConversationService _conversationService;
        private readonly IUserService _userService;
        private readonly ConnectionMapping<Guid> _connections;

        public ChatHub(
            IMessageService messageService,
            IConversationService conversationService,
            IUserService userService,
            ConnectionMapping<Guid> connections)
        {
            _messageService = messageService;
            _conversationService = conversationService;
            _userService = userService;
            _connections = connections;
        }

        public override async Task OnConnectedAsync()
        {
            if (Guid.TryParse(Context.UserIdentifier, out Guid userId))
            {
                var connectionCount = _connections.Add(userId, Context.ConnectionId);

                // ပထမဆုံး connection ဖြစ်မှသာ "online" broadcast လုပ် 
                // (device ၂ ခုနေရာက login ဝင်ထားရင် ၂ ခါ broadcast မလုပ်ဖို့)
                if (connectionCount == 1)
                {
                    await _userService.SetUserOnlineAsync(userId, true);

                    var contactIds = await _conversationService.GetConversationPartnerIdsAsync(userId);
                    foreach (var contactId in contactIds)
                    {
                        await Clients.User(contactId.ToString())
                            .SendAsync("UserStatusChanged", new
                            {
                                userId,
                                isOnline = true,
                                lastSeen = (DateTime?)null
                            });
                    }
                }

                Console.WriteLine($"User Connected:{userId} | ConnectionId :{Context.ConnectionId} | Total connections: {connectionCount}");
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Guid.TryParse(Context.UserIdentifier, out Guid userId))
            {
                var remainingConnections = _connections.Remove(userId, Context.ConnectionId);

                // connection အားလုံး ကုန်မှသာ "offline" broadcast လုပ်
                if (remainingConnections == 0)
                {
                    var lastSeen = DateTime.UtcNow;
                    await _userService.SetUserOnlineAsync(userId, false, lastSeen);

                    var contactIds = await _conversationService.GetConversationPartnerIdsAsync(userId);
                    foreach (var contactId in contactIds)
                    {
                        await Clients.User(contactId.ToString())
                            .SendAsync("UserStatusChanged", new
                            {
                                userId,
                                isOnline = false,
                                lastSeen
                            });
                    }
                }

                Console.WriteLine($"User Disconnected:{userId} | ConnectionId :{Context.ConnectionId} | Remaining: {remainingConnections}");
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(Guid receiverId, string content, Guid conversationId, string? attachmentUrl = null)
        {
            if (!Guid.TryParse(Context.UserIdentifier, out Guid senderId))
            {
                throw new HubException("User unauthorized.");
            }
            if (string.IsNullOrWhiteSpace(content) && string.IsNullOrEmpty(attachmentUrl))
            {
                throw new HubException("Cannot send an empty message.");
            }
            if (string.IsNullOrEmpty(attachmentUrl))
            {
                attachmentUrl = null;
            }

            var messageCreateDto = new MessageCreateDTO
            {
                ConversationId = conversationId,
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                AttachmentUrl = attachmentUrl
            };

            var response = await _messageService.SendMessageAsync(messageCreateDto, senderId);

            if (response.IsSuccess && response.Data != null)
            {
                await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", response.Data);
                await Clients.Caller.SendAsync("ReceiveMessage", response.Data);
            }
            else
            {
                await Clients.Caller.SendAsync("ErrorNotification", response.Message);
            }
        }
    }
}
