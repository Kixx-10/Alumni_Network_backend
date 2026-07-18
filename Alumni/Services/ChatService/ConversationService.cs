using Alumni.DTOs;
using Alumni.DTOS.Common;
using Alumni.Models.Master;
using Alumni.Repository.ChatRepository;
using AutoMapper;

namespace Alumni.Services.ChatService
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;
        public ConversationService(IConversationRepository conversationRepository, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ConversationReadDTO>> GetOrCreateConversationRoomAsync(Guid user1Id, Guid user2Id)
        {
            try
            {
                // check if a conversation already exists between the two users
                var conversation = await _conversationRepository.GetConversationBetweenUsersAsync(user1Id, user2Id);
                // if not, create a new conversation
                if (conversation == null)
                {
                    var newConversation = new Conversation
                    {
                        ConversationId = Guid.NewGuid(),
                        User1Id = user1Id,
                        User2Id = user2Id,
                        CreatedDate = DateTime.UtcNow
                    };
                    conversation = await _conversationRepository.CreateConversationAsync(newConversation);
                }
                var dto = _mapper.Map<ConversationReadDTO>(conversation);
                return ServiceResponse<ConversationReadDTO>.Success(dto, "Conversation retrieved or created successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<ConversationReadDTO>.Failure("CONVERSATION_ERROR", $"An error occurred while getting or creating the conversation: {ex.Message}");

            }
        }

        public async Task<ServiceResponse<IEnumerable<ConversationReadDTO>>> GetUserConversationListAsync(Guid userId)
        {
            try
            {
                var conversations = await _conversationRepository.GetUserConversationsAsync(userId);
                var dtos = _mapper.Map<IEnumerable<ConversationReadDTO>>(conversations, opts =>
                {
                    opts.Items["CurrentUserId"] = userId;
                });

                return ServiceResponse<IEnumerable<ConversationReadDTO>>.Success(dtos, "Inbox list retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<ConversationReadDTO>>.Failure("INBOX_LIST_ERROR", ex.Message);
            }
        }
    }
}