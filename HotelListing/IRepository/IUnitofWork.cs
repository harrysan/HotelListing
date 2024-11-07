using HotelListing.Data;

namespace HotelListing.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }
        Task Save();
    }
}
