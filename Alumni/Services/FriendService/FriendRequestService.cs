using Alumni.DTOS;
using Alumni.DTOS.Common;
using Alumni.Repository.FriendRepository;
using Alumni.Services.ChatService;
using AutoMapper;

namespace Alumni.Services.FriendService
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IMapper _mapper;
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IConversationService _conversationService;
        public FriendRequestService(IMapper mapper, IFriendRequestRepository friendRequestRepository, IConversationService conversationService)
        {
            _mapper = mapper;
            _friendRequestRepository = friendRequestRepository;
            _conversationService = conversationService;
        }
        public async Task<ServiceResponse<bool>> AcceptFriendRequestAsync(Guid requestId)
        {
            var response = new ServiceResponse<bool>();
            //check request exists or not 
            var request = await _friendRequestRepository.GetRequestByIdAsync(requestId);
            if (request == null)
            {
                response.IsSuccess = false;
                response.Message = "Friend request not found.";
                return response;
            }
            //if request is not pending, return error
            if (request.Status != "Pending")
            {
                response.IsSuccess = false;
                response.Message = $"This request has already been {request.Status.ToLower()}.";
                return response;
            }
            //update request status to accepted
            var updatedRequest = await _friendRequestRepository.UpdateRequestStatusAsync(requestId, "Accepted");
            if (updatedRequest == null)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong while accepting the friend request.";
                return response;
            }
            //if request is accepted, create a chat room for the two users
            var conversationResult = await _conversationService.GetOrCreateConversationRoomAsync(request.SenderId, request.ReceiverId);

            if (!conversationResult.IsSuccess)
            {
                response.Data = true;
                response.IsSuccess = true;
                response.Message = "Friend request accepted, but failed to create a chat room.";
                return response;
            }
            response.Data = true;
            response.IsSuccess = true;
            response.Message = "Friend request accepted and chat room created successfully.";

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<FriendRequestResponseDTO>>> GetPendingRequestsAsync(Guid userId)
        {
            var response = new ServiceResponse<IEnumerable<FriendRequestResponseDTO>>();

            var pendingRequests = await _friendRequestRepository.GetPendingRequestsAsync(userId);

            var pendingDtos = _mapper.Map<IEnumerable<FriendRequestResponseDTO>>(pendingRequests);
            response.Data = pendingDtos;
            response.IsSuccess = true;
            response.Message = "Pending friend requests retrieved successfully.";

            return response;
        }

        public async Task<ServiceResponse<bool>> RejectFriendRequestAsync(Guid requestId)
        {
            var response = new ServiceResponse<bool>();
            var request = await _friendRequestRepository.GetRequestByIdAsync(requestId);
            if (request == null)
            {
                response.IsSuccess = false;
                response.Message = "Friend request not found.";
                return response;
            }
            if (request.Status != "Pending")
            {
                response.IsSuccess = false;
                response.Message = $"This request has already been {request.Status.ToLower()}.";
                return response;
            }
            var updatedRequest = await _friendRequestRepository.UpdateRequestStatusAsync(requestId, "Rejected");
            if (updatedRequest == null)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong while rejecting the friend request.";
                return response;
            }
            response.Data = true;
            response.IsSuccess = true;
            response.Message = "Friend request rejected successfully.";

            return response;
        }

        public async Task<ServiceResponse<FriendRequestResponseDTO>> SendFriendRequestAsync(Guid senderId, FriendRequestCreateDTO createDto)
        {
            var response = new ServiceResponse<FriendRequestResponseDTO>();
            //not allow user to send friend request to himself
            if (senderId == createDto.ReceiverId)
            {
                response.IsSuccess = false;
                response.Message = "You cannot send a friend request to yourself.";
                return response;
            }
            //check if a friend request already exists between the sender and receiver
            var existingRequest = await _friendRequestRepository.GetExistingRequestAsync(senderId, createDto.ReceiverId);
            if (existingRequest != null)
            {
                response.IsSuccess = false;
                response.Message = existingRequest.Status == "Accepted"
                    ? "You are already friends with this user."
                    : "A friend request is already pending between you two.";
                return response;
            }
            var newRequest = await _friendRequestRepository.SendFriendRequestById(senderId, createDto.ReceiverId);
            if (newRequest == null)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong while sending the friend request.";
                return response;
            }
            var resultDto = _mapper.Map<FriendRequestResponseDTO>(newRequest);

            response.Data = resultDto;
            response.IsSuccess = true;
            response.Message = "Friend request sent successfully.";

            return response;
        }
    }
}
