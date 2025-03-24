using BookSwap.Enums;

namespace BookSwap.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public RequestStatus Status { get; set; }
    }
}
