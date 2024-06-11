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
            User user = GetAllAsync()
              .Result
              .Where(x => x.UserEmail == email)
              .FirstOrDefault();

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