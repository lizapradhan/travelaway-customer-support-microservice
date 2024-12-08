using CustomerSupportMicroservice.Models;

namespace CustomerSupportMicroservice.Interface
{
    public interface ISupportRepository
    {
        Task<int> AddQuery(string query, int bookingId, string userId);
        Task<IEnumerable<Support>> GetSupportDetailsByBookingId(int bookingId);
        Task<IEnumerable<Support>> GetSupportDetailsByUserId(string userId);
        Task AddReplyToCustomerQuery(string reply, int supportId);
        Task CloseSupportTicket(int supportId);
    }
}
