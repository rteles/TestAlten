using Alten.Booking.Application.Hotels.Queries.Interfaces;
using Alten.Booking.Application.Hotels.ViewModels;
using Alten.Booking.Domain.Hotels.Repositories;
using Alten.Booking.Infra.Adapters.Logger;
using AutoMapper;

namespace Alten.Booking.Application.Hotels.Queries;

public class HotelQueries : IHotelQueries
{
    private readonly IMapper _mapper;
    private readonly ILogAdapter _logAdapter;
    private readonly IHotelRepository _hotelRepository;

    public HotelQueries(IMapper mapper, ILogAdapter logAdapter, IHotelRepository hotelRepository)
    {
        _mapper = mapper;
        _logAdapter = logAdapter;
        _hotelRepository = hotelRepository;
    }

    public async Task<HotelViewModel?> Get(int id) =>
        _mapper.Map<HotelViewModel>(await _hotelRepository.GetById(id));

    public async Task<IEnumerable<HotelViewModel>> GetAll(bool onlyActives) =>
        _mapper.Map<IEnumerable<HotelViewModel>>(await _hotelRepository.GetAll(onlyActives));
}