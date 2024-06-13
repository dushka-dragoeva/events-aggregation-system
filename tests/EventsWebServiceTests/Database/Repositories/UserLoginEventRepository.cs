using EventsWebServiceTests.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebServiceTests.Database.Repositories
{
    public class UserLoginEventRepository : IntKeyRepository<UserLoginEvent>
    {
        private readonly EventsdbContext _context;
        private readonly DbSet<EventsdbContext> _dbSet;

        public UserLoginEventRepository(EventsdbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EventsdbContext>();
        }

        public async Task<UserLoginEvent> GetByUserIdAcync(string userId)
        {
            IEnumerable<UserLoginEvent> userLoginEvents = await GetAllAsync();
            UserLoginEvent userLoginEvent = userLoginEvents.FirstOrDefault(x => x.UserId == userId);

            return userLoginEvent;
        }

        public async Task DeleteByUserIdAsync(string userId)
        {
            var fileDownloadEvent = GetByUserIdAcync(userId).Result;
            if (fileDownloadEvent != null)
            {
                await DeleteAsync(fileDownloadEvent.Id);
            }
        }
    }
}
