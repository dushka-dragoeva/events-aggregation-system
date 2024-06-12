using EventsWebServiceTests.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebServiceTests.Database.Repositories
{
    public class FileDownloadEventRepository : IntKeyRepository<FileDownloadEvent>
    {
        private readonly EventsdbContext _context;
        private readonly DbSet<EventsdbContext> _dbSet;

        public FileDownloadEventRepository(EventsdbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EventsdbContext>();
        }

        public async Task<FileDownloadEvent> GetByEventIdAcync(string eventId)
        {
            IEnumerable<FileDownloadEvent> events = await GetAllAsync();
            FileDownloadEvent fileDownloadEvent = events.FirstOrDefault(x => x.EventId == eventId);

            return fileDownloadEvent;
        }

        public async Task DeleteByEventIdAsync(string eventId)
        {
            var fileDownloadEvent = GetByEventIdAcync(eventId).Result;
            if (fileDownloadEvent != null)
            {
                await DeleteAsync(fileDownloadEvent.Id);
            }
        }
    }
}
