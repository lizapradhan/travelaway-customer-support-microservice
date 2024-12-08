using CustomerSupportMicroservice.Interface;
using CustomerSupportMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportMicroservice.Repository
{
    public class SupportRepository: ISupportRepository
    {
        private readonly CustomerSupportDbContext _dbContext;
        public SupportRepository(CustomerSupportDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public async Task<int> AddQuery(string query, int bookingId, string userId)
        {
            try
            {
                var isQueryRaisedAlready = await _dbContext.Supports.AnyAsync(x => x.BookingId == bookingId && x.Status != 3);
                if (isQueryRaisedAlready)
                {
                    return 99;
                }
                var supportData = new Support
                {
                    BookingId = bookingId,
                    Query = query,
                    Status = 1,
                    Assignee = "admin@gmail.com",
                    CreatedBy = userId
                };
                await _dbContext.Supports.AddAsync(supportData);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Support>> GetSupportDetailsByBookingId(int bookingId)
        {
            try
            {
                return await _dbContext.Supports.Where(x => x.BookingId == bookingId).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Support>> GetSupportDetailsByUserId(string userId)
        {
            try
            {
                return await _dbContext.Supports.Where(x => x.CreatedBy == userId).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddReplyToCustomerQuery(string reply, int supportId)
        {
            try
            {
                var supportData = await _dbContext.Supports.FindAsync(supportId);
                if (supportData != null) 
                {
                    supportData.Status = 2;
                    supportData.Reply = reply;
                }

                _dbContext.Supports.Update(supportData);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CloseSupportTicket(int supportId)
        {
            try
            {
                var supportData = await _dbContext.Supports.FindAsync(supportId);
                if (supportData != null)
                {
                    supportData.Status = 3;
                }

                _dbContext.Supports.Update(supportData);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
