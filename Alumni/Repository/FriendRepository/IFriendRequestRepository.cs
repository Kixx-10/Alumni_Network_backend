using Alumni.Models.Core;
using Alumni.Models.Master;

namespace Alumni.Repository.FriendRepository
{
    public interface IFriendRequestRepository
    {
        Task<FriendRequest?> SendFriendRequestById(Guid senderId, Guid receiverId);

        //Status change method
        Task<FriendRequest?> UpdateRequestStatusAsync(Guid requestId, string status);

        //To search all request with only id 
        Task<FriendRequest?> GetRequestByIdAsync(Guid requestId);

        //To get all pending requests for a user
        Task<IEnumerable<FriendRequest>> GetPendingRequestsAsync(Guid userId);

        // check if a friend request already exists between two users
        Task<FriendRequest?> GetExistingRequestAsync(Guid senderId, Guid receiverId);

        Task<IEnumerable<User>> GetDiscoverableUsersAsync(Guid currentUserId);

    }
}
