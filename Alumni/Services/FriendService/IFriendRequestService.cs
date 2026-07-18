using Alumni.DTOS;
using Alumni.DTOS.Common;

namespace Alumni.Services.FriendService
{
    public interface IFriendRequestService
    {
        // Sending friend request
        Task<ServiceResponse<FriendRequestResponseDTO>> SendFriendRequestAsync(Guid senderId, FriendRequestCreateDTO createDto);

        // accepting friend request
        Task<ServiceResponse<bool>> AcceptFriendRequestAsync(Guid requestId);

        // rejecting friend request
        Task<ServiceResponse<bool>> RejectFriendRequestAsync(Guid requestId);

        // take pending friend requests for a user
        Task<ServiceResponse<IEnumerable<FriendRequestResponseDTO>>> GetPendingRequestsAsync(Guid userId);
        Task<ServiceResponse<IEnumerable<UserDiscoverDTO>>> GetDiscoverableUsersAsync(Guid userId);
    }
}
