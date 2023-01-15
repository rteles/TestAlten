using Alten.Booking.Application.Hotels.ViewModels;

namespace Alten.Booking.Application.Hotels.Queries.Interfaces;

public interface IHotelQueries
{
    Task<HotelViewModel?> Get(int id);
    Task<IEnumerable<HotelViewModel>> GetAll(bool onlyActives);
}