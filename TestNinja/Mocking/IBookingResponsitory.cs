using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingRespository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }
}