namespace BookSwap.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
        public List<Friendship> Friends { get; set; } = new List<Friendship>();

        public List<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public List<FriendRequest> ReceivedFriendRequests { get; set; } = new List<FriendRequest>();

        public List<TradeRequest> SentTradeRequests { get; set; } = new List<TradeRequest>();
        public List<TradeRequest> ReceivedTradeRequests { get; set; } = new List<TradeRequest>();
    }
}
