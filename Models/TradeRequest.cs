using BookSwap.Enums;

namespace BookSwap.Models
{
    public class TradeRequest
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
        public int SenderBookId { get; set; }
        public int ReceiverBookId { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime SentAt { get; set; }
    }
}
