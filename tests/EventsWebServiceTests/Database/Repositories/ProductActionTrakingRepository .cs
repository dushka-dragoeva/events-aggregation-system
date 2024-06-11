using EventsWebServiceTests.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebServiceTests.Database.Repositories
{
    public class ProductActionTrakingRepository : IntKeyRepository<ProductActionTraking>
    {
        private readonly EventsdbContext _context;
        private readonly DbSet<EventsdbContext> _dbSet;

        public ProductActionTrakingRepository(EventsdbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EventsdbContext>();
        }

        public async Task<ProductActionTraking> GetByUserIdAcync(string userId)
        {
            ProductActionTraking productAction = GetAllAsync()
              .Result
              .Where(x => x.UserId == userId)
              .FirstOrDefault();

            return productAction;
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