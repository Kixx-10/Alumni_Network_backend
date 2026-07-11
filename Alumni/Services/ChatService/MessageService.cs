using Alumni.DTOs;
using Alumni.DTOS.Common;
using Alumni.Models.Master;
using Alumni.Repository.ChatRepository;
using AutoMapper;

namespace Alumni.Services.ChatService
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IConversationRepository _conversationRepository;
        public MessageService(IMessageRepository messageRepository, IMapper mapper, IConversationRepository conversationRepository)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;// to show latest message in the chat list, we need to update the conversation's last message when a new message is created
            _mapper = mapper;
        }
        public async Task<ServiceResponse<MessageReadDTO>> SendMessageAsync(MessageCreateDTO createDto, Guid senderId)
        {
            try
            {
                var message = _mapper.Map<Message>(createDto);
                message.MessageId = Guid.NewGuid();
                message.SenderId = senderId;
                message.MessageStatus = "Sent";
                message.CreatedDate = DateTime.UtcNow;


                var savedMessage = await _messageRepository.CreateMessageAsync(message);

                await _conversationRepository.UpdateConversationLastMessageAsync(savedMessage.ConversationId, savedMessage.MessageId);


                var dto = _mapper.Map<MessageReadDTO>(savedMessage);
                return ServiceResponse<MessageReadDTO>.Success(dto, "Message sent successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<MessageReadDTO>.Failure("SEND_MESSAGE_ERROR", ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<MessageReadDTO>>> GetChatHistoryAsync(Guid conversationId, int limit = 50)
        {
            try
            {
                var messages = await _messageRepository.GetMessagesByConversationIdAsync(conversationId, limit);

                var dtos = _mapper.Map<IEnumerable<MessageReadDTO>>(messages);

                return ServiceResponse<IEnumerable<MessageReadDTO>>.Success(dtos, "Chat history retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<MessageReadDTO>>.Failure("GET_CHAT_HISTORY_ERROR", ex.Message);
            }
        }

        public async Task<ServiceResponse<MessageReadDTO>> ReadMessageAsync(Guid messageId, Guid userId)
        {
            try
            {
                // search message
                var message = await _messageRepository.ReadMessageAsync(messageId);
                if (message == null)
                {
                    return ServiceResponse<MessageReadDTO>.Failure("MESSAGE_NOT_FOUND", "Message not found.");
                }
                if (message.SenderId != userId && message.ReceiverId != userId)
                {
                    return ServiceResponse<MessageReadDTO>.Failure("UNAUTHORIZED", "You are not authorized to read this message.");
                }
                if (message.ReceiverId == userId && message.MessageStatus == "Sent")
                {
                    message.MessageStatus = "Read";
                    message.UpdatedDate = DateTime.UtcNow;
                }
                var dto = _mapper.Map<MessageReadDTO>(message);
                return ServiceResponse<MessageReadDTO>.Success(dto, "Message read successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<MessageReadDTO>.Failure("READ_MESSAGE_ERROR", ex.Message);
            }
        }
    }
}

