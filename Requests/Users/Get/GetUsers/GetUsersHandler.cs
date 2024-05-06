using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Users.Get
{

    public class GetUsersHandler : IRequestHandler<GetUsersRequest, Result<UserDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetUsersHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<UserDTO[]>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _db.Users
            .Include(user => user.Image)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            return _mapper.Map<UserDTO[]>(users);
        }
    }
}