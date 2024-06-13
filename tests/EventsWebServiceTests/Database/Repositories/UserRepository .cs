using EventsWebServiceTests.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebServiceTests.Database.Repositories
{
    public class UserRepository : GuidKeyRepository<User>
    {
        private readonly EventsdbContext _context;
        private readonly DbSet<EventsdbContext> _dbSet;

        public UserRepository(EventsdbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EventsdbContext>();
        }

        public async Task<User> GetByEmailAcync(string email)
        {
            IEnumerable<User> users = await GetAllAsync();
            User user = users.FirstOrDefault(x => x.UserEmail == email);

            return user;
        }

        public async Task DeleteByEmailAsync(string email)
        {
            var user = GetByEmailAcync(email).Result;
            if (user != null)
            {
                await DeleteAsync(user.Id);
            }
        }
    }
}