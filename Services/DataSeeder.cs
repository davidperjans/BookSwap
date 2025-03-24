using BookSwap.Data;

namespace BookSwap.Services
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;
        private readonly FakeDataService _fakeDataService;

        public DataSeeder(AppDbContext context, FakeDataService fakeDataService)
        {
            _context = context;
            _fakeDataService = fakeDataService;
        }

        public void Seed()
        {
            // Generera fejkanvändare
            var users = _fakeDataService.GenerateUsers(10);
            _context.Users.AddRange(users);
            _context.SaveChanges();

            // Generera fejkade vänförfrågningar
            var friendRequests = _fakeDataService.GenerateFriendRequests(users);
            _context.FriendRequests.AddRange(friendRequests);
            _context.SaveChanges();

            // Skapa vänskap baserat på accepterade förfrågningar
            var friendships = _fakeDataService.CreateFriendships(friendRequests);
            _context.Friendships.AddRange(friendships);
            _context.SaveChanges();
        }
    }
}
