using BookSwap.Data;
using BookSwap.Enums;
using BookSwap.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.Services
{
    public class TradeRequestService : ITradeRequestService
    {
        private readonly AppDbContext _context;

        public TradeRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> SendTradeRequestAsync(int senderId, int receiverId, int senderBookId, int receiverBookId)
        {
            var serviceResult = new ServiceResult();

            var senderBook = await _context.Books.FindAsync(senderBookId);
            var receiverBook = await _context.Books.FindAsync(receiverBookId);

            if (senderBook == null || receiverBook == null)
            {
                serviceResult.IsSuccess = false;
                serviceResult.ErrorMessage = "One or both books not found";
                return serviceResult;
            }

            var tradeRequest = new TradeRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                SenderBookId = senderBookId,
                ReceiverBookId = receiverBookId,
                Status = RequestStatus.Pending,
                SentAt = DateTime.UtcNow,
            };

            _context.TradeRequests.Add(tradeRequest);
            await _context.SaveChangesAsync();

            serviceResult.IsSuccess = true;
            serviceResult.Message = "Trade request sent successfully";
            return serviceResult;
        }

        public async Task<ServiceResult> AcceptTradeRequestAsync(int tradeRequestId, int receiverId)
        {
            var serviceResult = new ServiceResult();

            var tradeRequest = await _context.TradeRequests.FirstOrDefaultAsync(tradeRequest => tradeRequest.Id == tradeRequestId && tradeRequest.ReceiverId == receiverId);

            if (tradeRequest == null)
            {
                serviceResult.IsSuccess = false;
                serviceResult.ErrorMessage = "Trade request not found or already accepted/rejected";
                return serviceResult;
            }

            tradeRequest.Status = RequestStatus.Accepted;

            var senderBook = await _context.Books.FindAsync(tradeRequest.SenderBookId);
            var receiverBook = await _context.Books.FindAsync(tradeRequest.ReceiverBookId);

            senderBook.OwnerId = receiverId;
            receiverBook.OwnerId = tradeRequest.SenderId;

            await _context.SaveChangesAsync();

            serviceResult.IsSuccess = true;
            serviceResult.Message = "Trade request accepted, books exchanged!";
            return serviceResult;

        }

        public async Task<ServiceResult> RejectTradeRequestAsync(int tradeRequestId, int receiverId)
        {
            var serviceResult = new ServiceResult();

            var tradeRequest = await _context.TradeRequests.FirstOrDefaultAsync(tradeRequest => tradeRequest.Id == tradeRequestId && tradeRequest.ReceiverId == receiverId);

            if (tradeRequest == null)
            {
                serviceResult.IsSuccess = false;
                serviceResult.ErrorMessage = "Trade request not found or already accepted/rejected";
                return serviceResult;
            }

            tradeRequest.Status = RequestStatus.Rejected;
            await _context.SaveChangesAsync();

            serviceResult.IsSuccess = true;
            serviceResult.Message = "Trade request rejected";
            return serviceResult;

        }

        public async Task<IEnumerable<TradeRequest>> GetPendingTradeRequestsByUserAsync(int userId)
        {
            return await _context.TradeRequests.Where(tradeRequest => tradeRequest.ReceiverId == userId).ToListAsync();
        }
    }
}
