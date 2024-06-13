using EventsWebServiceTests.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebServiceTests.Database.Repositories
{
    public class UserLogoutEventRepository : IntKeyRepository<UserLogOutEvent>
    {
        private readonly EventsdbContext _context;
        private readonly DbSet<EventsdbContext> _dbSet;

        public UserLogoutEventRepository(EventsdbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EventsdbContext>();
        }

        public async Task<UserLogOutEvent> GetByEmailAcync(string email)
        {
            IEnumerable<UserLogOutEvent> userLogoutEvents = await GetAllAsync();
            UserLogOutEvent userLogoutEvent = userLogoutEvents.FirstOrDefault(x => x.Email == email);

            return userLogoutEvent;
        }

        public async Task DeleteByEmailAsync(string email)
        {
            var userLogoutEvent = GetByEmailAcync(email).Result;
            if (userLogoutEvent != null)
            {
                await DeleteAsync(userLogoutEvent.Id);
            }
        }
    }
}