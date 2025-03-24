using BookSwap.Models;

namespace BookSwap.Services
{
    public interface IFriendRequestService
    {
        Task<ServiceResult> SendFriendRequestAsync(int senderId, int receiverId);
        Task<ServiceResult> AcceptFriendRequestAsync(int requestId, int receiverId);
        Task<ServiceResult> RejectFriendRequestAsync(int requestId, int receiverId);
        Task<IEnumerable<FriendRequest>> GetPendingFriendRequestsAsync(int userId);
    }
}
