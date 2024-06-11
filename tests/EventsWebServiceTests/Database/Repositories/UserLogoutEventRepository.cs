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
            UserLogOutEvent userLogoutEvent = GetAllAsync()
              .Result
              .Where(x => x.Email == email)
              .FirstOrDefault();

            return userLogoutEvent;
        }

        public async Task DeleteByEmailAsync(string email)
        {
            var  userLogoutEvent = GetByEmailAcync(email).Result;
            if ( userLogoutEvent != null)
            {
                await DeleteAsync( userLogoutEvent.Id);
            }
        }
    }
}