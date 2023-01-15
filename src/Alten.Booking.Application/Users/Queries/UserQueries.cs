using Alten.Booking.Application.Users.Queries.Interfaces;
using Alten.Booking.Application.Users.ViewModels;
using Alten.Booking.Domain.Users.Repositories;
using Alten.Booking.Infra.Adapters.Logger;
using AutoMapper;

namespace Alten.Booking.Application.Users.Queries;

public class UserQueries : IUserQueries
{
    private readonly IMapper _mapper;
    private readonly ILogAdapter _logAdapter;
    private readonly IUserRepository _userRepository;

    public UserQueries(IMapper mapper, ILogAdapter logAdapter, IUserRepository userRepository)
    {
        _mapper = mapper;
        _logAdapter = logAdapter;
        _userRepository = userRepository;
    }

    public async Task<UserViewModel?> Get(int id) => _mapper.Map<UserViewModel>(await _userRepository.GetById(id));

    public async Task<IEnumerable<UserViewModel>> Get(bool onlyActives) =>
        _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetAll(onlyActives));
}