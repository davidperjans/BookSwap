using Bogus;
using BookSwap.Enums;
using BookSwap.Models;

namespace BookSwap.Services
{
    public class FakeDataService
    {
        private readonly Random _random = new Random();

        public List<User> GenerateUsers(int numberOfUsers)
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Books, f => new List<Book>()) // Om du vill lägga till böcker senare
                .RuleFor(u => u.Friends, f => new List<Friendship>())
                .RuleFor(u => u.SentFriendRequests, f => new List<FriendRequest>())
                .RuleFor(u => u.ReceivedFriendRequests, f => new List<FriendRequest>())
                .RuleFor(u => u.SentTradeRequests, f => new List<TradeRequest>())
                .RuleFor(u => u.ReceivedTradeRequests, f => new List<TradeRequest>());

            return userFaker.Generate(numberOfUsers);
        }

        // Generera fejkade vänförfrågningar
        public List<FriendRequest> GenerateFriendRequests(List<User> users)
        {
            var friendRequestFaker = new Faker<FriendRequest>()
                .RuleFor(fr => fr.SenderId, f => users[f.Random.Number(users.Count - 1)].Id)
                .RuleFor(fr => fr.ReceiverId, f => users[f.Random.Number(users.Count - 1)].Id)
                .RuleFor(fr => fr.Status, f => f.PickRandom<RequestStatus>());

            return friendRequestFaker.Generate(users.Count);
        }

        // Metod för att skapa vänskap genom att acceptera vänförfrågningar
        public List<Friendship> CreateFriendships(List<FriendRequest> friendRequests)
        {
            var friendships = new List<Friendship>();

            foreach (var request in friendRequests.Where(fr => fr.Status == RequestStatus.Accepted))
            {
                var friendship = new Friendship
                {
                    User1Id = request.SenderId,
                    User2Id = request.ReceiverId
                };
                friendships.Add(friendship);
            }

            return friendships;
        }
    }
}
