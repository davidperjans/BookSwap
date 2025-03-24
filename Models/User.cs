namespace BookSwap.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public List<FriendRequest> FriendRequests { get; set; } = new List<FriendRequest>();
        public List<TradeRequest> TradeRequests { get; set; } = new List<TradeRequest>();
    }
}
