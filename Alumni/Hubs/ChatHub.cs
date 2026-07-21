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

                // ပထမဆုံး Connection ဖြစ်မှသာ Online Status Broadcast 
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

                Console.WriteLine($"[ONLINE] User: {userId} | ConnectionId: {Context.ConnectionId} | Total: {connectionCount}");
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Guid.TryParse(Context.UserIdentifier, out Guid userId))
            {
                var remainingConnections = _connections.Remove(userId, Context.ConnectionId);

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

                Console.WriteLine($"[OFFLINE] User: {userId} | Remaining Connections: {remainingConnections}");
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string receiverIdStr, string content, string conversationIdStr, string? attachmentUrl = null)
        {
            //   Sender Authentication Check
            if (!Guid.TryParse(Context.UserIdentifier, out Guid senderId))
            {
                throw new HubException("User unauthorized.");
            }

            // GUID Parameters Parsing Check
            if (!Guid.TryParse(receiverIdStr, out Guid receiverId) || !Guid.TryParse(conversationIdStr, out Guid conversationId))
            {
                throw new HubException("Invalid Receiver or Conversation Identifier.");
            }

            //  Content Validation
            if (string.IsNullOrWhiteSpace(content) && string.IsNullOrEmpty(attachmentUrl))
            {
                throw new HubException("Cannot send an empty message.");
            }

            if (string.IsNullOrWhiteSpace(attachmentUrl))
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

            //  Database Save Operation
            var response = await _messageService.SendMessageAsync(messageCreateDto, senderId);

            //  Real-time Message Broadcast
            if (response.IsSuccess && response.Data != null)
            {
                // Receiver ဘက်သို့ Message ပို့ပေးခြင်း
                await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", response.Data);
                // Sender (Caller) ဘက်သို့ တုံ့ပြန်ချက် ပြန်ပို့ပေးခြင်း
                await Clients.Caller.SendAsync("ReceiveMessage", response.Data);
            }
            else
            {
                await Clients.Caller.SendAsync("ErrorNotification", response.Message);
            }
        }
    }
}