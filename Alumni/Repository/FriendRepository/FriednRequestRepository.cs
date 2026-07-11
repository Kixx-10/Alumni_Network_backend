using Alumni.Data;
using Alumni.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Repository.FriendRepository
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly AppDbContext _context;
        public FriendRequestRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<FriendRequest?> SendFriendRequestById(Guid senderId, Guid receiverId)
        {
            var frienRequest = new FriendRequest
            {
                Id = Guid.NewGuid(),
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = "Pending",
                CreatedDate = DateTime.UtcNow

            };
            _context.FriendRequests.Add(frienRequest);
            await _context.SaveChangesAsync();
            return frienRequest;
        }

        public async Task<FriendRequest?> UpdateRequestStatusAsync(Guid requestId, string status)
        {
            var existingRequest = await _context.FriendRequests
                .Where(fr => fr.Id == requestId)
                .FirstOrDefaultAsync();
            if (existingRequest == null)
            {
                return null;
            }
            existingRequest.Status = status;
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return existingRequest;
            }

            return null;
        }
        public async Task<FriendRequest?> GetExistingRequestAsync(Guid senderId, Guid receiverId)
        {
            var existingRequest = await _context.FriendRequests
                 .Where(fr => (fr.SenderId == senderId && fr.ReceiverId == receiverId) ||
                              (fr.SenderId == receiverId && fr.ReceiverId == senderId))
                 .FirstOrDefaultAsync();
            return existingRequest;
        }

        public async Task<IEnumerable<FriendRequest>> GetPendingRequestsAsync(Guid userId)
        {
            var pendingRequests = await _context.FriendRequests
                .Include(fr => fr.Sender)
                .Where(fr => fr.ReceiverId == userId && fr.Status == "Pending")
                .ToListAsync();

            return pendingRequests;
        }

        public async Task<FriendRequest?> GetRequestByIdAsync(Guid requestId)
        {
            var request = await _context.FriendRequests
                .Where(fr => fr.Id == requestId)
                .FirstOrDefaultAsync();
            return request;
        }
    }
}
