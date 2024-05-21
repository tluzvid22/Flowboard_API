using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Users.Get
{

    public class GetUserByEmailAndPasswordHandler : IRequestHandler<GetUserByEmailAndPasswordRequest, Result<UserDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetUserByEmailAndPasswordHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<UserDTO>> Handle(GetUserByEmailAndPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .Include(user => user.Image)
                .Include(user => user.Workspaces)
                .AsNoTracking().
                FirstOrDefaultAsync(user => user.Email == request.email && user.Password == request.password, cancellationToken);

            return _mapper.Map<UserDTO>(user);
        }
    }
}