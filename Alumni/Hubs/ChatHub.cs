using Alumni.DTOs;
using Alumni.Services.ChatService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Alumni.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IConversationService _conversationService;
        public ChatHub(IMessageService messageService, IConversationService conversationService)
        {
            _messageService = messageService;
            _conversationService = conversationService;
        }
        // when user connects to the hub, this method will be called
        public override async Task OnConnectedAsync()
        {
            //get userId from jwt token
            var userId = Context.UserIdentifier;
            Console.WriteLine($"User Connected:{userId} | ConnectionId :{Context.ConnectionId}");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            Console.WriteLine($"User Disconnected:{userId} | ConnectionId :{Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(Guid receiverId, string content, Guid conversationId)
        {
            if (!Guid.TryParse(Context.UserIdentifier, out Guid senderId))
            {
                throw new HubException("User unauthorized.");
            }

            var messageCreateDto = new MessageCreateDTO
            {
                ConversationId = conversationId,
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content
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
