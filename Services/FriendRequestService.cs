using BookSwap.Data;
using BookSwap.Enums;
using BookSwap.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.Services
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly AppDbContext _context;

        public FriendRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> SendFriendRequestAsync(int senderId, int recieverId)
        {
            var serviceResult = new ServiceResult();
            var existingRequest = _context.FriendRequests.FirstOrDefault(fr => fr.SenderId == senderId && fr.ReceiverId == recieverId);

            if (existingRequest != null)
            {
                serviceResult.IsSuccess = false;
                serviceResult.ErrorMessage = "Friend request already exists";
            }
            else
            {
                var friendRequest = new FriendRequest
                {
                    SenderId = senderId,
                    ReceiverId = recieverId,
                    Status = RequestStatus.Pending,
                    SentAt = DateTime.Now
                };

                _context.FriendRequests.Add(friendRequest);
                await _context.SaveChangesAsync();

                serviceResult.IsSuccess = true;
                serviceResult.Message = "Friend request sent";
            }
            return serviceResult;
        }

        public async Task<ServiceResult> AcceptFriendRequestAsync(int requestId, int receiverId)
        {
            var serviceResult = new ServiceResult();

            var friendRequest = await _context.FriendRequests
                .FirstOrDefaultAsync(fr => fr.Id == requestId && fr.ReceiverId == receiverId);

            if (friendRequest == null)
            {
                serviceResult.IsSuccess = false;
                serviceResult.ErrorMessage = "Friend request not found";
                return serviceResult;
            }

            // Uppdatera status
            friendRequest.Status = RequestStatus.Accepted;

            // Lägg till vänskap i Friendship-tabellen
            var friendship = new Friendship
            {
                User1Id = friendRequest.SenderId,
                User2Id = friendRequest.ReceiverId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();

            serviceResult.IsSuccess = true;
            serviceResult.Message = "Friend request accepted and friendship created";
            return serviceResult;
        }

        public async Task<ServiceResult> RejectFriendRequestAsync(int requestId, int recieverId)
        {
            var serviceResult = new ServiceResult();

            var friendRequest = _context.FriendRequests.FirstOrDefault(fr => fr.Id == requestId && fr.ReceiverId == recieverId);

            if (friendRequest == null)
            {
                serviceResult.IsSuccess = false;
                serviceResult.ErrorMessage = "Friend request not found";
            }
            friendRequest!.Status = RequestStatus.Rejected;
            await _context.SaveChangesAsync();
            serviceResult.IsSuccess = true;
            serviceResult.Message = "Friend request rejected";
            return serviceResult;
        }

        public async Task<IEnumerable<FriendRequest>> GetPendingFriendRequestsAsync(int userId)
        {
            return _context.FriendRequests
                .Where(fr => fr.ReceiverId == userId && fr.Status == RequestStatus.Pending)
                .ToList();
        }
    }
}
