using BookSwap.Models;

namespace BookSwap.Services
{
    public interface ITradeRequestService
    {
        Task<ServiceResult> SendTradeRequestAsync(int senderId, int receiverId, int senderBookId, int receiverBookId);
        Task<ServiceResult> AcceptTradeRequestAsync(int tradeRequestId, int receiverId);
        Task<ServiceResult> RejectTradeRequestAsync(int tradeRequestId, int receiverId);
        Task<IEnumerable<TradeRequest>> GetPendingTradeRequestsByUserAsync(int userId);
    }
}
